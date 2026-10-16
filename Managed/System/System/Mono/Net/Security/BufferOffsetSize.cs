using System;

namespace Mono.Net.Security
{
	// Token: 0x0200008A RID: 138
	internal class BufferOffsetSize
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00006233 File Offset: 0x00004433
		public int EndOffset
		{
			get
			{
				return this.Offset + this.Size;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00006242 File Offset: 0x00004442
		public int Remaining
		{
			get
			{
				return this.Buffer.Length - this.Offset - this.Size;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000625C File Offset: 0x0000445C
		public BufferOffsetSize(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || offset + size > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
			this.Complete = false;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000062BF File Offset: 0x000044BF
		public override string ToString()
		{
			return string.Format("[BufferOffsetSize: {0} {1}]", this.Offset, this.Size);
		}

		// Token: 0x0400021D RID: 541
		public byte[] Buffer;

		// Token: 0x0400021E RID: 542
		public int Offset;

		// Token: 0x0400021F RID: 543
		public int Size;

		// Token: 0x04000220 RID: 544
		public int TotalBytes;

		// Token: 0x04000221 RID: 545
		public bool Complete;
	}
}
