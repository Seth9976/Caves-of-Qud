using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000130 RID: 304
	internal readonly struct MethodPtrTableReader
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
		internal MethodPtrTableReader(int numberOfRows, int methodTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsMethodTableRowRefSizeSmall = methodTableRowRefSize == 2;
			this._MethodOffset = 0;
			this.RowSize = this._MethodOffset + methodTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001E9E4 File Offset: 0x0001CBE4
		internal MethodDefinitionHandle GetMethodFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return MethodDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._MethodOffset, this._IsMethodTableRowRefSizeSmall));
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001EA1A File Offset: 0x0001CC1A
		internal int GetRowIdForMethodDefRow(int methodDefRowId)
		{
			return this.Block.LinearSearchReference(this.RowSize, this._MethodOffset, (uint)methodDefRowId, this._IsMethodTableRowRefSizeSmall) + 1;
		}

		// Token: 0x04000776 RID: 1910
		internal readonly int NumberOfRows;

		// Token: 0x04000777 RID: 1911
		private readonly bool _IsMethodTableRowRefSizeSmall;

		// Token: 0x04000778 RID: 1912
		private readonly int _MethodOffset;

		// Token: 0x04000779 RID: 1913
		internal readonly int RowSize;

		// Token: 0x0400077A RID: 1914
		internal readonly MemoryBlock Block;
	}
}
