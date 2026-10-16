using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x020001F7 RID: 503
	internal class WaveOutBuffer : IDisposable
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x000224B4 File Offset: 0x000206B4
		public WaveOutBuffer(IntPtr hWaveOut, int bufferSize, IWaveProvider bufferFillStream, object waveOutLock)
		{
			this.bufferSize = bufferSize;
			this.buffer = new byte[bufferSize];
			this.hBuffer = GCHandle.Alloc(this.buffer, 3);
			this.hWaveOut = hWaveOut;
			this.waveStream = bufferFillStream;
			this.waveOutLock = waveOutLock;
			this.header = new WaveHeader();
			this.hHeader = GCHandle.Alloc(this.header, 3);
			this.header.dataBuffer = this.hBuffer.AddrOfPinnedObject();
			this.header.bufferLength = bufferSize;
			this.header.loops = 1;
			this.hThis = GCHandle.Alloc(this);
			this.header.userData = (IntPtr)this.hThis;
			lock (waveOutLock)
			{
				MmException.Try(WaveInterop.waveOutPrepareHeader(hWaveOut, this.header, Marshal.SizeOf(this.header)), "waveOutPrepareHeader");
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000225B4 File Offset: 0x000207B4
		~WaveOutBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000225E4 File Offset: 0x000207E4
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000225F4 File Offset: 0x000207F4
		protected void Dispose(bool disposing)
		{
			if (this.hHeader.IsAllocated)
			{
				this.hHeader.Free();
			}
			if (this.hBuffer.IsAllocated)
			{
				this.hBuffer.Free();
			}
			if (this.hThis.IsAllocated)
			{
				this.hThis.Free();
			}
			if (this.hWaveOut != IntPtr.Zero)
			{
				lock (this.waveOutLock)
				{
					WaveInterop.waveOutUnprepareHeader(this.hWaveOut, this.header, Marshal.SizeOf(this.header));
				}
				this.hWaveOut = IntPtr.Zero;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000226AC File Offset: 0x000208AC
		internal bool OnDone()
		{
			int num;
			lock (this.waveStream)
			{
				num = this.waveStream.Read(this.buffer, 0, this.buffer.Length);
			}
			if (num == 0)
			{
				return false;
			}
			for (int i = num; i < this.buffer.Length; i++)
			{
				this.buffer[i] = 0;
			}
			this.WriteToWaveOut();
			return true;
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00022724 File Offset: 0x00020924
		public bool InQueue
		{
			get
			{
				return (this.header.flags & WaveHeaderFlags.InQueue) == WaveHeaderFlags.InQueue;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x00022738 File Offset: 0x00020938
		public int BufferSize
		{
			get
			{
				return this.bufferSize;
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00022740 File Offset: 0x00020940
		private void WriteToWaveOut()
		{
			MmResult mmResult;
			lock (this.waveOutLock)
			{
				mmResult = WaveInterop.waveOutWrite(this.hWaveOut, this.header, Marshal.SizeOf(this.header));
			}
			if (mmResult != MmResult.NoError)
			{
				throw new MmException(mmResult, "waveOutWrite");
			}
			GC.KeepAlive(this);
		}

		// Token: 0x04000BE9 RID: 3049
		private readonly WaveHeader header;

		// Token: 0x04000BEA RID: 3050
		private readonly int bufferSize;

		// Token: 0x04000BEB RID: 3051
		private readonly byte[] buffer;

		// Token: 0x04000BEC RID: 3052
		private readonly IWaveProvider waveStream;

		// Token: 0x04000BED RID: 3053
		private readonly object waveOutLock;

		// Token: 0x04000BEE RID: 3054
		private GCHandle hBuffer;

		// Token: 0x04000BEF RID: 3055
		private IntPtr hWaveOut;

		// Token: 0x04000BF0 RID: 3056
		private GCHandle hHeader;

		// Token: 0x04000BF1 RID: 3057
		private GCHandle hThis;
	}
}
