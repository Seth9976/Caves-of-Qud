using System;
using System.IO;

namespace Ionic.BZip2
{
	// Token: 0x02000041 RID: 65
	internal class BitWriter
	{
		// Token: 0x0600032D RID: 813 RVA: 0x00011364 File Offset: 0x0000F564
		public BitWriter(Stream s)
		{
			this.output = s;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00011373 File Offset: 0x0000F573
		public byte RemainingBits
		{
			get
			{
				return (byte)((this.accumulator >> 32 - this.nAccumulatedBits) & 255U);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0001138F File Offset: 0x0000F58F
		public int NumRemainingBits
		{
			get
			{
				return this.nAccumulatedBits;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00011397 File Offset: 0x0000F597
		public int TotalBytesWrittenOut
		{
			get
			{
				return this.totalBytesWrittenOut;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001139F File Offset: 0x0000F59F
		public void Reset()
		{
			this.accumulator = 0U;
			this.nAccumulatedBits = 0;
			this.totalBytesWrittenOut = 0;
			this.output.Seek(0L, 0);
			this.output.SetLength(0L);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public void WriteBits(int nbits, uint value)
		{
			int i = this.nAccumulatedBits;
			uint num = this.accumulator;
			while (i >= 8)
			{
				this.output.WriteByte((byte)((num >> 24) & 255U));
				this.totalBytesWrittenOut++;
				num <<= 8;
				i -= 8;
			}
			this.accumulator = num | (value << 32 - i - nbits);
			this.nAccumulatedBits = i + nbits;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001143D File Offset: 0x0000F63D
		public void WriteByte(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00011448 File Offset: 0x0000F648
		public void WriteInt(uint u)
		{
			this.WriteBits(8, (u >> 24) & 255U);
			this.WriteBits(8, (u >> 16) & 255U);
			this.WriteBits(8, (u >> 8) & 255U);
			this.WriteBits(8, u & 255U);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00011495 File Offset: 0x0000F695
		public void Flush()
		{
			this.WriteBits(0, 0U);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000114A0 File Offset: 0x0000F6A0
		public void FinishAndPad()
		{
			this.Flush();
			if (this.NumRemainingBits > 0)
			{
				byte b = (byte)((this.accumulator >> 24) & 255U);
				this.output.WriteByte(b);
				this.totalBytesWrittenOut++;
			}
		}

		// Token: 0x040001A7 RID: 423
		private uint accumulator;

		// Token: 0x040001A8 RID: 424
		private int nAccumulatedBits;

		// Token: 0x040001A9 RID: 425
		private Stream output;

		// Token: 0x040001AA RID: 426
		private int totalBytesWrittenOut;
	}
}
