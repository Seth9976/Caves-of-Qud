using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x020001F4 RID: 500
	internal class WaveInBuffer : IDisposable
	{
		// Token: 0x06000B43 RID: 2883 RVA: 0x00021958 File Offset: 0x0001FB58
		public WaveInBuffer(IntPtr waveInHandle, int bufferSize)
		{
			this.bufferSize = bufferSize;
			this.buffer = new byte[bufferSize];
			this.hBuffer = GCHandle.Alloc(this.buffer, 3);
			this.waveInHandle = waveInHandle;
			this.header = new WaveHeader();
			this.hHeader = GCHandle.Alloc(this.header, 3);
			this.header.dataBuffer = this.hBuffer.AddrOfPinnedObject();
			this.header.bufferLength = bufferSize;
			this.header.loops = 1;
			this.hThis = GCHandle.Alloc(this);
			this.header.userData = (IntPtr)this.hThis;
			MmException.Try(WaveInterop.waveInPrepareHeader(waveInHandle, this.header, Marshal.SizeOf(this.header)), "waveInPrepareHeader");
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00021A28 File Offset: 0x0001FC28
		public void Reuse()
		{
			MmException.Try(WaveInterop.waveInUnprepareHeader(this.waveInHandle, this.header, Marshal.SizeOf(this.header)), "waveUnprepareHeader");
			MmException.Try(WaveInterop.waveInPrepareHeader(this.waveInHandle, this.header, Marshal.SizeOf(this.header)), "waveInPrepareHeader");
			MmException.Try(WaveInterop.waveInAddBuffer(this.waveInHandle, this.header, Marshal.SizeOf(this.header)), "waveInAddBuffer");
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00021AA8 File Offset: 0x0001FCA8
		~WaveInBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00021AD8 File Offset: 0x0001FCD8
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		protected void Dispose(bool disposing)
		{
			if (this.waveInHandle != IntPtr.Zero)
			{
				WaveInterop.waveInUnprepareHeader(this.waveInHandle, this.header, Marshal.SizeOf(this.header));
				this.waveInHandle = IntPtr.Zero;
			}
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
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00021B79 File Offset: 0x0001FD79
		public byte[] Data
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00021B81 File Offset: 0x0001FD81
		public bool Done
		{
			get
			{
				return (this.header.flags & WaveHeaderFlags.Done) == WaveHeaderFlags.Done;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00021B93 File Offset: 0x0001FD93
		public bool InQueue
		{
			get
			{
				return (this.header.flags & WaveHeaderFlags.InQueue) == WaveHeaderFlags.InQueue;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00021BA7 File Offset: 0x0001FDA7
		public int BytesRecorded
		{
			get
			{
				return this.header.bytesRecorded;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00021BB4 File Offset: 0x0001FDB4
		public int BufferSize
		{
			get
			{
				return this.bufferSize;
			}
		}

		// Token: 0x04000BD0 RID: 3024
		private readonly WaveHeader header;

		// Token: 0x04000BD1 RID: 3025
		private readonly int bufferSize;

		// Token: 0x04000BD2 RID: 3026
		private readonly byte[] buffer;

		// Token: 0x04000BD3 RID: 3027
		private GCHandle hBuffer;

		// Token: 0x04000BD4 RID: 3028
		private IntPtr waveInHandle;

		// Token: 0x04000BD5 RID: 3029
		private GCHandle hHeader;

		// Token: 0x04000BD6 RID: 3030
		private GCHandle hThis;
	}
}
