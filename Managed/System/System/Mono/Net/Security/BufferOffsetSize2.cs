using System;

namespace Mono.Net.Security
{
	// Token: 0x0200008B RID: 139
	internal class BufferOffsetSize2 : BufferOffsetSize
	{
		// Token: 0x06000223 RID: 547 RVA: 0x000062E1 File Offset: 0x000044E1
		public BufferOffsetSize2(int size)
			: base(new byte[size], 0, 0)
		{
			this.InitialSize = size;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000062F8 File Offset: 0x000044F8
		public void Reset()
		{
			this.Offset = (this.Size = 0);
			this.TotalBytes = 0;
			this.Buffer = new byte[this.InitialSize];
			this.Complete = false;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006334 File Offset: 0x00004534
		public void MakeRoom(int size)
		{
			if (base.Remaining >= size)
			{
				return;
			}
			int num = size - base.Remaining;
			if (this.Offset == 0 && this.Size == 0)
			{
				this.Buffer = new byte[size];
				return;
			}
			byte[] array = new byte[this.Buffer.Length + num];
			this.Buffer.CopyTo(array, 0);
			this.Buffer = array;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006395 File Offset: 0x00004595
		public void AppendData(byte[] buffer, int offset, int size)
		{
			this.MakeRoom(size);
			global::System.Buffer.BlockCopy(buffer, offset, this.Buffer, base.EndOffset, size);
			this.Size += size;
		}

		// Token: 0x04000222 RID: 546
		public readonly int InitialSize;
	}
}
