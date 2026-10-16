using System;
using System.Runtime.InteropServices;
using NAudio.Utils;
using NAudio.Wave;

namespace NAudio.MediaFoundation
{
	// Token: 0x020001CC RID: 460
	public abstract class MediaFoundationTransform : IWaveProvider, IDisposable
	{
		// Token: 0x060009EC RID: 2540 RVA: 0x0001C980 File Offset: 0x0001AB80
		public MediaFoundationTransform(IWaveProvider sourceProvider, WaveFormat outputFormat)
		{
			this.outputWaveFormat = outputFormat;
			this.sourceProvider = sourceProvider;
			this.sourceBuffer = new byte[sourceProvider.WaveFormat.AverageBytesPerSecond];
			this.outputBuffer = new byte[this.outputWaveFormat.AverageBytesPerSecond + this.outputWaveFormat.BlockAlign];
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		private void InitializeTransformForStreaming()
		{
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_COMMAND_FLUSH, IntPtr.Zero);
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_BEGIN_STREAMING, IntPtr.Zero);
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_START_OF_STREAM, IntPtr.Zero);
			this.initializedForStreaming = true;
		}

		// Token: 0x060009EE RID: 2542
		protected abstract IMFTransform CreateTransform();

		// Token: 0x060009EF RID: 2543 RVA: 0x0001CA2B File Offset: 0x0001AC2B
		protected virtual void Dispose(bool disposing)
		{
			if (this.transform != null)
			{
				Marshal.ReleaseComObject(this.transform);
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001CA41 File Offset: 0x0001AC41
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001CA60 File Offset: 0x0001AC60
		~MediaFoundationTransform()
		{
			this.Dispose(false);
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0001CA90 File Offset: 0x0001AC90
		public WaveFormat WaveFormat
		{
			get
			{
				return this.outputWaveFormat;
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001CA98 File Offset: 0x0001AC98
		public int Read(byte[] buffer, int offset, int count)
		{
			if (this.transform == null)
			{
				this.transform = this.CreateTransform();
				this.InitializeTransformForStreaming();
			}
			int i = 0;
			if (this.outputBufferCount > 0)
			{
				i += this.ReadFromOutputBuffer(buffer, offset, count - i);
			}
			while (i < count)
			{
				IMFSample imfsample = this.ReadFromSource();
				if (imfsample == null)
				{
					this.EndStreamAndDrain();
					i += this.ReadFromOutputBuffer(buffer, offset + i, count - i);
					break;
				}
				if (!this.initializedForStreaming)
				{
					this.InitializeTransformForStreaming();
				}
				this.transform.ProcessInput(0, imfsample, 0);
				Marshal.ReleaseComObject(imfsample);
				this.ReadFromTransform();
				i += this.ReadFromOutputBuffer(buffer, offset + i, count - i);
			}
			return i;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		private void EndStreamAndDrain()
		{
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_END_OF_STREAM, IntPtr.Zero);
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_COMMAND_DRAIN, IntPtr.Zero);
			int num;
			do
			{
				num = this.ReadFromTransform();
			}
			while (num > 0);
			this.outputBufferCount = 0;
			this.outputBufferOffset = 0;
			this.inputPosition = 0L;
			this.outputPosition = 0L;
			this.transform.ProcessMessage(MFT_MESSAGE_TYPE.MFT_MESSAGE_NOTIFY_END_STREAMING, IntPtr.Zero);
			this.initializedForStreaming = false;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001CBB4 File Offset: 0x0001ADB4
		private int ReadFromTransform()
		{
			MFT_OUTPUT_DATA_BUFFER[] array = new MFT_OUTPUT_DATA_BUFFER[1];
			IMFSample imfsample = MediaFoundationApi.CreateSample();
			IMFMediaBuffer imfmediaBuffer = MediaFoundationApi.CreateMemoryBuffer(this.outputBuffer.Length);
			imfsample.AddBuffer(imfmediaBuffer);
			imfsample.SetSampleTime(this.outputPosition);
			array[0].pSample = imfsample;
			_MFT_PROCESS_OUTPUT_STATUS mft_PROCESS_OUTPUT_STATUS;
			int num = this.transform.ProcessOutput(_MFT_PROCESS_OUTPUT_FLAGS.None, 1, array, out mft_PROCESS_OUTPUT_STATUS);
			if (num == -1072861838)
			{
				Marshal.ReleaseComObject(imfmediaBuffer);
				Marshal.ReleaseComObject(imfsample);
				return 0;
			}
			if (num != 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			IMFMediaBuffer imfmediaBuffer2;
			array[0].pSample.ConvertToContiguousBuffer(out imfmediaBuffer2);
			IntPtr intPtr;
			int num2;
			int num3;
			imfmediaBuffer2.Lock(out intPtr, out num2, out num3);
			this.outputBuffer = BufferHelpers.Ensure(this.outputBuffer, num3);
			Marshal.Copy(intPtr, this.outputBuffer, 0, num3);
			this.outputBufferOffset = 0;
			this.outputBufferCount = num3;
			imfmediaBuffer2.Unlock();
			this.outputPosition += MediaFoundationTransform.BytesToNsPosition(this.outputBufferCount, this.WaveFormat);
			Marshal.ReleaseComObject(imfmediaBuffer);
			Marshal.ReleaseComObject(imfsample);
			Marshal.ReleaseComObject(imfmediaBuffer2);
			return num3;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		private static long BytesToNsPosition(int bytes, WaveFormat waveFormat)
		{
			return 10000000L * (long)bytes / (long)waveFormat.AverageBytesPerSecond;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
		private IMFSample ReadFromSource()
		{
			int num = this.sourceProvider.Read(this.sourceBuffer, 0, this.sourceBuffer.Length);
			if (num == 0)
			{
				return null;
			}
			IMFMediaBuffer imfmediaBuffer = MediaFoundationApi.CreateMemoryBuffer(num);
			IntPtr intPtr;
			int num2;
			int num3;
			imfmediaBuffer.Lock(out intPtr, out num2, out num3);
			Marshal.Copy(this.sourceBuffer, 0, intPtr, num);
			imfmediaBuffer.Unlock();
			imfmediaBuffer.SetCurrentLength(num);
			IMFSample imfsample = MediaFoundationApi.CreateSample();
			imfsample.AddBuffer(imfmediaBuffer);
			imfsample.SetSampleTime(this.inputPosition);
			long num4 = MediaFoundationTransform.BytesToNsPosition(num, this.sourceProvider.WaveFormat);
			imfsample.SetSampleDuration(num4);
			this.inputPosition += num4;
			Marshal.ReleaseComObject(imfmediaBuffer);
			return imfsample;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001CD90 File Offset: 0x0001AF90
		private int ReadFromOutputBuffer(byte[] buffer, int offset, int needed)
		{
			int num = Math.Min(needed, this.outputBufferCount);
			Array.Copy(this.outputBuffer, this.outputBufferOffset, buffer, offset, num);
			this.outputBufferOffset += num;
			this.outputBufferCount -= num;
			if (this.outputBufferCount == 0)
			{
				this.outputBufferOffset = 0;
			}
			return num;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001CDEA File Offset: 0x0001AFEA
		public void Reposition()
		{
			if (this.initializedForStreaming)
			{
				this.EndStreamAndDrain();
				this.InitializeTransformForStreaming();
			}
		}

		// Token: 0x04000B1F RID: 2847
		protected readonly IWaveProvider sourceProvider;

		// Token: 0x04000B20 RID: 2848
		protected readonly WaveFormat outputWaveFormat;

		// Token: 0x04000B21 RID: 2849
		private readonly byte[] sourceBuffer;

		// Token: 0x04000B22 RID: 2850
		private byte[] outputBuffer;

		// Token: 0x04000B23 RID: 2851
		private int outputBufferOffset;

		// Token: 0x04000B24 RID: 2852
		private int outputBufferCount;

		// Token: 0x04000B25 RID: 2853
		private IMFTransform transform;

		// Token: 0x04000B26 RID: 2854
		private bool disposed;

		// Token: 0x04000B27 RID: 2855
		private long inputPosition;

		// Token: 0x04000B28 RID: 2856
		private long outputPosition;

		// Token: 0x04000B29 RID: 2857
		private bool initializedForStreaming;
	}
}
