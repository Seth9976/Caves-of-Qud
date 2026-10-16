using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NAudio.MediaFoundation;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001AA RID: 426
	public class MediaFoundationEncoder : IDisposable
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x000196A8 File Offset: 0x000178A8
		public static int[] GetEncodeBitrates(Guid audioSubtype, int sampleRate, int channels)
		{
			return Enumerable.ToArray<int>(Enumerable.OrderBy<int, int>(Enumerable.Distinct<int>(Enumerable.Select<MediaType, int>(Enumerable.Where<MediaType>(MediaFoundationEncoder.GetOutputMediaTypes(audioSubtype), (MediaType mt) => mt.SampleRate == sampleRate && mt.ChannelCount == channels), (MediaType mt) => mt.AverageBytesPerSecond * 8)), (int br) => br));
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00019730 File Offset: 0x00017930
		public static MediaType[] GetOutputMediaTypes(Guid audioSubtype)
		{
			IMFCollection imfcollection;
			try
			{
				MediaFoundationInterop.MFTranscodeGetAudioOutputAvailableTypes(audioSubtype, _MFT_ENUM_FLAG.MFT_ENUM_FLAG_ALL, null, out imfcollection);
			}
			catch (COMException ex)
			{
				if (ex.GetHResult() == -1072875819)
				{
					return new MediaType[0];
				}
				throw;
			}
			int num;
			imfcollection.GetElementCount(out num);
			List<MediaType> list = new List<MediaType>(num);
			for (int i = 0; i < num; i++)
			{
				object obj;
				imfcollection.GetElement(i, out obj);
				IMFMediaType imfmediaType = (IMFMediaType)obj;
				list.Add(new MediaType(imfmediaType));
			}
			Marshal.ReleaseComObject(imfcollection);
			return list.ToArray();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000197C4 File Offset: 0x000179C4
		public static void EncodeToWma(IWaveProvider inputProvider, string outputFile, int desiredBitRate = 192000)
		{
			MediaType mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_WMAudioV8, inputProvider.WaveFormat, desiredBitRate);
			if (mediaType == null)
			{
				throw new InvalidOperationException("No suitable WMA encoders available");
			}
			using (MediaFoundationEncoder mediaFoundationEncoder = new MediaFoundationEncoder(mediaType))
			{
				mediaFoundationEncoder.Encode(outputFile, inputProvider);
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001981C File Offset: 0x00017A1C
		public static void EncodeToMp3(IWaveProvider inputProvider, string outputFile, int desiredBitRate = 192000)
		{
			MediaType mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_MP3, inputProvider.WaveFormat, desiredBitRate);
			if (mediaType == null)
			{
				throw new InvalidOperationException("No suitable MP3 encoders available");
			}
			using (MediaFoundationEncoder mediaFoundationEncoder = new MediaFoundationEncoder(mediaType))
			{
				mediaFoundationEncoder.Encode(outputFile, inputProvider);
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00019874 File Offset: 0x00017A74
		public static void EncodeToAac(IWaveProvider inputProvider, string outputFile, int desiredBitRate = 192000)
		{
			MediaType mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_AAC, inputProvider.WaveFormat, desiredBitRate);
			if (mediaType == null)
			{
				throw new InvalidOperationException("No suitable AAC encoders available");
			}
			using (MediaFoundationEncoder mediaFoundationEncoder = new MediaFoundationEncoder(mediaType))
			{
				mediaFoundationEncoder.Encode(outputFile, inputProvider);
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00019A44 File Offset: 0x00017C44
		public static MediaType SelectMediaType(Guid audioSubtype, WaveFormat inputFormat, int desiredBitRate)
		{
			return Enumerable.FirstOrDefault<MediaType>(Enumerable.Select(Enumerable.OrderBy(Enumerable.Select(Enumerable.Where<MediaType>(MediaFoundationEncoder.GetOutputMediaTypes(audioSubtype), (MediaType mt) => mt.SampleRate == inputFormat.SampleRate && mt.ChannelCount == inputFormat.Channels), (MediaType mt) => new
			{
				MediaType = mt,
				Delta = Math.Abs(desiredBitRate - mt.AverageBytesPerSecond * 8)
			}), mt => mt.Delta), mt => mt.MediaType));
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00019AD6 File Offset: 0x00017CD6
		public MediaFoundationEncoder(MediaType outputMediaType)
		{
			if (outputMediaType == null)
			{
				throw new ArgumentNullException("outputMediaType");
			}
			this.outputMediaType = outputMediaType;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00019AF4 File Offset: 0x00017CF4
		public void Encode(string outputFile, IWaveProvider inputProvider)
		{
			if (inputProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm && inputProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Encode input format must be PCM or IEEE float");
			}
			MediaType mediaType = new MediaType(inputProvider.WaveFormat);
			IMFSinkWriter imfsinkWriter = MediaFoundationEncoder.CreateSinkWriter(outputFile);
			try
			{
				int num;
				imfsinkWriter.AddStream(this.outputMediaType.MediaFoundationObject, out num);
				imfsinkWriter.SetInputMediaType(num, mediaType.MediaFoundationObject, null);
				this.PerformEncode(imfsinkWriter, num, inputProvider);
			}
			finally
			{
				Marshal.ReleaseComObject(imfsinkWriter);
				Marshal.ReleaseComObject(mediaType.MediaFoundationObject);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00019B8C File Offset: 0x00017D8C
		private static IMFSinkWriter CreateSinkWriter(string outputFile)
		{
			IMFAttributes imfattributes = MediaFoundationApi.CreateAttributes(1);
			imfattributes.SetUINT32(MediaFoundationAttributes.MF_READWRITE_ENABLE_HARDWARE_TRANSFORMS, 1);
			IMFSinkWriter imfsinkWriter;
			try
			{
				MediaFoundationInterop.MFCreateSinkWriterFromURL(outputFile, null, imfattributes, out imfsinkWriter);
			}
			catch (COMException ex)
			{
				if (ex.GetHResult() == -1072875819)
				{
					throw new ArgumentException("Was not able to create a sink writer for this file extension");
				}
				throw;
			}
			finally
			{
				Marshal.ReleaseComObject(imfattributes);
			}
			return imfsinkWriter;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00019BFC File Offset: 0x00017DFC
		private void PerformEncode(IMFSinkWriter writer, int streamIndex, IWaveProvider inputProvider)
		{
			int num = inputProvider.WaveFormat.AverageBytesPerSecond * 4;
			byte[] array = new byte[num];
			writer.BeginWriting();
			long num2 = 0L;
			long num3;
			do
			{
				num3 = this.ConvertOneBuffer(writer, streamIndex, inputProvider, num2, array);
				num2 += num3;
			}
			while (num3 > 0L);
			writer.DoFinalize();
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00019C48 File Offset: 0x00017E48
		private static long BytesToNsPosition(int bytes, WaveFormat waveFormat)
		{
			return 10000000L * (long)bytes / (long)waveFormat.AverageBytesPerSecond;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00019C68 File Offset: 0x00017E68
		private long ConvertOneBuffer(IMFSinkWriter writer, int streamIndex, IWaveProvider inputProvider, long position, byte[] managedBuffer)
		{
			long num = 0L;
			IMFMediaBuffer imfmediaBuffer = MediaFoundationApi.CreateMemoryBuffer(managedBuffer.Length);
			int num2;
			imfmediaBuffer.GetMaxLength(out num2);
			IMFSample imfsample = MediaFoundationApi.CreateSample();
			imfsample.AddBuffer(imfmediaBuffer);
			IntPtr intPtr;
			int num3;
			imfmediaBuffer.Lock(out intPtr, out num2, out num3);
			int num4 = inputProvider.Read(managedBuffer, 0, num2);
			if (num4 > 0)
			{
				num = MediaFoundationEncoder.BytesToNsPosition(num4, inputProvider.WaveFormat);
				Marshal.Copy(managedBuffer, 0, intPtr, num4);
				imfmediaBuffer.SetCurrentLength(num4);
				imfmediaBuffer.Unlock();
				imfsample.SetSampleTime(position);
				imfsample.SetSampleDuration(num);
				writer.WriteSample(streamIndex, imfsample);
			}
			else
			{
				imfmediaBuffer.Unlock();
			}
			Marshal.ReleaseComObject(imfsample);
			Marshal.ReleaseComObject(imfmediaBuffer);
			return num;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00019D0A File Offset: 0x00017F0A
		protected void Dispose(bool disposing)
		{
			Marshal.ReleaseComObject(this.outputMediaType.MediaFoundationObject);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00019D1D File Offset: 0x00017F1D
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.Dispose(true);
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00019D3C File Offset: 0x00017F3C
		~MediaFoundationEncoder()
		{
			this.Dispose(false);
		}

		// Token: 0x04000A76 RID: 2678
		private readonly MediaType outputMediaType;

		// Token: 0x04000A77 RID: 2679
		private bool disposed;
	}
}
