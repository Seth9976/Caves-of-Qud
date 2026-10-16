using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000055 RID: 85
	internal class DataStream
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000431E File Offset: 0x0000251E
		internal int Length
		{
			get
			{
				return this.contentSize;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00002F43 File Offset: 0x00001143
		internal DataStream()
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004326 File Offset: 0x00002526
		internal DataStream(int contentSize, BitAccess bits, int count)
		{
			this.contentSize = contentSize;
			if (count > 0)
			{
				this.pages = new int[count];
				bits.ReadInt32(this.pages);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004351 File Offset: 0x00002551
		internal void Read(PdbReader reader, BitAccess bits)
		{
			bits.MinCapacity(this.contentSize);
			this.Read(reader, 0, bits.Buffer, 0, this.contentSize);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004374 File Offset: 0x00002574
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

		// Token: 0x04000168 RID: 360
		internal int contentSize;

		// Token: 0x04000169 RID: 361
		internal int[] pages;
	}
}
