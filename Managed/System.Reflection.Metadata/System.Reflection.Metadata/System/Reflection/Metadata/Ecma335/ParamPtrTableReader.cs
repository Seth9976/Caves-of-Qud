using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000132 RID: 306
	internal readonly struct ParamPtrTableReader
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x0001EC29 File Offset: 0x0001CE29
		internal ParamPtrTableReader(int numberOfRows, int paramTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsParamTableRowRefSizeSmall = paramTableRowRefSize == 2;
			this._ParamOffset = 0;
			this.RowSize = this._ParamOffset + paramTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001EC68 File Offset: 0x0001CE68
		internal ParameterHandle GetParamFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return ParameterHandle.FromRowId(this.Block.PeekReference(num + this._ParamOffset, this._IsParamTableRowRefSizeSmall));
		}

		// Token: 0x04000787 RID: 1927
		internal readonly int NumberOfRows;

		// Token: 0x04000788 RID: 1928
		private readonly bool _IsParamTableRowRefSizeSmall;

		// Token: 0x04000789 RID: 1929
		private readonly int _ParamOffset;

		// Token: 0x0400078A RID: 1930
		internal readonly int RowSize;

		// Token: 0x0400078B RID: 1931
		internal readonly MemoryBlock Block;
	}
}
