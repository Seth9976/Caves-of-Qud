using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000338 RID: 824
	internal struct BitSet
	{
		// Token: 0x0600159A RID: 5530 RVA: 0x00043D0F File Offset: 0x00041F0F
		internal BitSet(BitAccess bits)
		{
			bits.ReadInt32(out this.size);
			this.words = new uint[this.size];
			bits.ReadUInt32(this.words);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00043D3C File Offset: 0x00041F3C
		internal bool IsSet(int index)
		{
			int num = index / 32;
			return num < this.size && (this.words[num] & BitSet.GetBit(index)) > 0U;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00043D6B File Offset: 0x00041F6B
		private static uint GetBit(int index)
		{
			return 1U << index % 32;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00043D76 File Offset: 0x00041F76
		internal bool IsEmpty
		{
			get
			{
				return this.size == 0;
			}
		}

		// Token: 0x04000A9E RID: 2718
		private int size;

		// Token: 0x04000A9F RID: 2719
		private uint[] words;
	}
}
