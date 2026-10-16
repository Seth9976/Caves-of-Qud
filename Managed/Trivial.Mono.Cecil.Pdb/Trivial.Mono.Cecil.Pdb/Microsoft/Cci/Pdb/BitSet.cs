using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200001E RID: 30
	internal struct BitSet
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000042AD File Offset: 0x000024AD
		internal bool IsEmpty
		{
			get
			{
				return this.size == 0;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000042B8 File Offset: 0x000024B8
		internal BitSet(BitAccess bits)
		{
			bits.ReadInt32(out this.size);
			this.words = new uint[this.size];
			bits.ReadUInt32(this.words);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000042E4 File Offset: 0x000024E4
		internal bool IsSet(int index)
		{
			int num = index / 32;
			return num < this.size && (this.words[num] & BitSet.GetBit(index)) > 0U;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004313 File Offset: 0x00002513
		private static uint GetBit(int index)
		{
			return 1U << index % 32;
		}

		// Token: 0x04000047 RID: 71
		private int size;

		// Token: 0x04000048 RID: 72
		private uint[] words;
	}
}
