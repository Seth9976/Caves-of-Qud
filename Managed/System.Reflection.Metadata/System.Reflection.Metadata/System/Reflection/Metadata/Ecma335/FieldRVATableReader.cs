using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000148 RID: 328
	internal readonly struct FieldRVATableReader
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x000204C4 File Offset: 0x0001E6C4
		internal FieldRVATableReader(int numberOfRows, bool declaredSorted, int fieldTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsFieldTableRowRefSizeSmall = fieldTableRowRefSize == 2;
			this._RvaOffset = 0;
			this._FieldOffset = this._RvaOffset + 4;
			this.RowSize = this._FieldOffset + fieldTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.FieldRva);
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00020530 File Offset: 0x0001E730
		internal int GetRva(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekInt32(num + this._RvaOffset);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002055C File Offset: 0x0001E75C
		internal int FindFieldRvaRowId(int fieldDefRowId)
		{
			int num = this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._FieldOffset, (uint)fieldDefRowId, this._IsFieldTableRowRefSizeSmall);
			return num + 1;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00020591 File Offset: 0x0001E791
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._FieldOffset, this._IsFieldTableRowRefSizeSmall);
		}

		// Token: 0x04000822 RID: 2082
		internal readonly int NumberOfRows;

		// Token: 0x04000823 RID: 2083
		private readonly bool _IsFieldTableRowRefSizeSmall;

		// Token: 0x04000824 RID: 2084
		private readonly int _RvaOffset;

		// Token: 0x04000825 RID: 2085
		private readonly int _FieldOffset;

		// Token: 0x04000826 RID: 2086
		internal readonly int RowSize;

		// Token: 0x04000827 RID: 2087
		internal readonly MemoryBlock Block;
	}
}
