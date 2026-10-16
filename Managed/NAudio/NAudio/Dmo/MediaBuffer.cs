using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	// Token: 0x02000090 RID: 144
	public class MediaBuffer : IMediaBuffer, IDisposable
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000A243 File Offset: 0x00008443
		public MediaBuffer(int maxLength)
		{
			this.buffer = Marshal.AllocCoTaskMem(maxLength);
			this.maxLength = maxLength;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A25E File Offset: 0x0000845E
		public void Dispose()
		{
			if (this.buffer != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.buffer);
				this.buffer = IntPtr.Zero;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A290 File Offset: 0x00008490
		~MediaBuffer()
		{
			this.Dispose();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A2BC File Offset: 0x000084BC
		int IMediaBuffer.SetLength(int length)
		{
			if (length > this.maxLength)
			{
				return -2147483645;
			}
			this.length = length;
			return 0;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A2D5 File Offset: 0x000084D5
		int IMediaBuffer.GetMaxLength(out int maxLength)
		{
			maxLength = this.maxLength;
			return 0;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A2E0 File Offset: 0x000084E0
		int IMediaBuffer.GetBufferAndLength(IntPtr bufferPointerPointer, IntPtr validDataLengthPointer)
		{
			if (bufferPointerPointer != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(bufferPointerPointer, this.buffer);
			}
			if (validDataLengthPointer != IntPtr.Zero)
			{
				Marshal.WriteInt32(validDataLengthPointer, this.length);
			}
			return 0;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000A315 File Offset: 0x00008515
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000A31D File Offset: 0x0000851D
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				if (this.length > this.maxLength)
				{
					throw new ArgumentException("Cannot be greater than maximum buffer size");
				}
				this.length = value;
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000A33F File Offset: 0x0000853F
		public void LoadData(byte[] data, int bytes)
		{
			this.Length = bytes;
			Marshal.Copy(data, 0, this.buffer, bytes);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000A356 File Offset: 0x00008556
		public void RetrieveData(byte[] data, int offset)
		{
			Marshal.Copy(this.buffer, data, offset, this.Length);
		}

		// Token: 0x040003FB RID: 1019
		private IntPtr buffer;

		// Token: 0x040003FC RID: 1020
		private int length;

		// Token: 0x040003FD RID: 1021
		private int maxLength;
	}
}
