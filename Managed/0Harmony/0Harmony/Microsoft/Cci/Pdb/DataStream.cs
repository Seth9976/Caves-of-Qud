using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003F3 RID: 1011
	internal class DataStream
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x00002AED File Offset: 0x00000CED
		internal DataStream()
		{
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00043D81 File Offset: 0x00041F81
		internal DataStream(int contentSize, BitAccess bits, int count)
		{
			this.contentSize = contentSize;
			if (count > 0)
			{
				this.pages = new int[count];
				bits.ReadInt32(this.pages);
			}
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00043DAC File Offset: 0x00041FAC
		internal void Read(PdbReader reader, BitAccess bits)
		{
			bits.MinCapacity(this.contentSize);
			this.Read(reader, 0, bits.Buffer, 0, this.contentSize);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00043DD0 File Offset: 0x00041FD0
		internal void Read(PdbReader reader, int position, byte[] bytes, int offset, int data)
		{
			if (position + data > this.contentSize)
			{
				throw new PdbException("DataStream can't read off end of stream. (pos={0},siz={1})", new object[] { position, data });
			}
			if (position == this.contentSize)
			{
				return;
			}
			int i = data;
			int num = position / reader.pageSize;
			int num2 = position % reader.pageSize;
			if (num2 != 0)
			{
				int num3 = reader.pageSize - num2;
				if (num3 > i)
				{
					num3 = i;
				}
				reader.Seek(this.pages[num], num2);
				reader.Read(bytes, offset, num3);
				offset += num3;
				i -= num3;
				num++;
			}
			while (i > 0)
			{
				int num4 = reader.pageSize;
				if (num4 > i)
				{
					num4 = i;
				}
				reader.Seek(this.pages[num], 0);
				reader.Read(bytes, offset, num4);
				offset += num4;
				i -= num4;
				num++;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00043EA5 File Offset: 0x000420A5
		internal int Length
		{
			get
			{
				return this.contentSize;
			}
		}

		// Token: 0x04000EFE RID: 3838
		internal int contentSize;

		// Token: 0x04000EFF RID: 3839
		internal int[] pages;
	}
}
