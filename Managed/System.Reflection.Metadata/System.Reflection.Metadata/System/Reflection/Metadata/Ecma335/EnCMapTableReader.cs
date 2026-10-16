using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200014A RID: 330
	internal readonly struct EnCMapTableReader
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002065F File Offset: 0x0001E85F
		internal EnCMapTableReader(int numberOfRows, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._TokenOffset = 0;
			this.RowSize = this._TokenOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00020694 File Offset: 0x0001E894
		internal uint GetToken(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._TokenOffset);
		}

		// Token: 0x0400082D RID: 2093
		internal readonly int NumberOfRows;

		// Token: 0x0400082E RID: 2094
		private readonly int _TokenOffset;

		// Token: 0x0400082F RID: 2095
		internal readonly int RowSize;

		// Token: 0x04000830 RID: 2096
		internal readonly MemoryBlock Block;
	}
}
