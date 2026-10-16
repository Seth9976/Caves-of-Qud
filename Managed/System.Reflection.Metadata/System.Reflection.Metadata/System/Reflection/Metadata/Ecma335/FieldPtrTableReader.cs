using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200012E RID: 302
	internal readonly struct FieldPtrTableReader
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x0001E7F1 File Offset: 0x0001C9F1
		internal FieldPtrTableReader(int numberOfRows, int fieldTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsFieldTableRowRefSizeSmall = fieldTableRowRefSize == 2;
			this._FieldOffset = 0;
			this.RowSize = this._FieldOffset + fieldTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001E830 File Offset: 0x0001CA30
		internal FieldDefinitionHandle GetFieldFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return FieldDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._FieldOffset, this._IsFieldTableRowRefSizeSmall));
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001E866 File Offset: 0x0001CA66
		internal int GetRowIdForFieldDefRow(int fieldDefRowId)
		{
			return this.Block.LinearSearchReference(this.RowSize, this._FieldOffset, (uint)fieldDefRowId, this._IsFieldTableRowRefSizeSmall) + 1;
		}

		// Token: 0x04000769 RID: 1897
		internal readonly int NumberOfRows;

		// Token: 0x0400076A RID: 1898
		private readonly bool _IsFieldTableRowRefSizeSmall;

		// Token: 0x0400076B RID: 1899
		private readonly int _FieldOffset;

		// Token: 0x0400076C RID: 1900
		internal readonly int RowSize;

		// Token: 0x0400076D RID: 1901
		internal readonly MemoryBlock Block;
	}
}
