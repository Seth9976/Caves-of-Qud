using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000149 RID: 329
	internal readonly struct EnCLogTableReader
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x000205B0 File Offset: 0x0001E7B0
		internal EnCLogTableReader(int numberOfRows, MemoryBlock containingBlock, int containingBlockOffset, MetadataStreamKind metadataStreamKind)
		{
			this.NumberOfRows = ((metadataStreamKind == MetadataStreamKind.Compressed) ? 0 : numberOfRows);
			this._TokenOffset = 0;
			this._FuncCodeOffset = this._TokenOffset + 4;
			this.RowSize = this._FuncCodeOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00020608 File Offset: 0x0001E808
		internal uint GetToken(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._TokenOffset);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00020634 File Offset: 0x0001E834
		internal EditAndContinueOperation GetFuncCode(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return (EditAndContinueOperation)this.Block.PeekUInt32(num + this._FuncCodeOffset);
		}

		// Token: 0x04000828 RID: 2088
		internal readonly int NumberOfRows;

		// Token: 0x04000829 RID: 2089
		private readonly int _TokenOffset;

		// Token: 0x0400082A RID: 2090
		private readonly int _FuncCodeOffset;

		// Token: 0x0400082B RID: 2091
		internal readonly int RowSize;

		// Token: 0x0400082C RID: 2092
		internal readonly MemoryBlock Block;
	}
}
