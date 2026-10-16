using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000154 RID: 340
	internal readonly struct NestedClassTableReader
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x00021080 File Offset: 0x0001F280
		internal NestedClassTableReader(int numberOfRows, bool declaredSorted, int typeDefTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = typeDefTableRowRefSize == 2;
			this._NestedClassOffset = 0;
			this._EnclosingClassOffset = this._NestedClassOffset + typeDefTableRowRefSize;
			this.RowSize = this._EnclosingClassOffset + typeDefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.NestedClass);
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000210EC File Offset: 0x0001F2EC
		internal TypeDefinitionHandle GetNestedClass(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._NestedClassOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00021124 File Offset: 0x0001F324
		internal TypeDefinitionHandle GetEnclosingClass(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._EnclosingClassOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002115C File Offset: 0x0001F35C
		internal TypeDefinitionHandle FindEnclosingType(TypeDefinitionHandle nestedTypeDef)
		{
			int num = this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._NestedClassOffset, (uint)nestedTypeDef.RowId, this._IsTypeDefTableRowRefSizeSmall);
			if (num == -1)
			{
				return default(TypeDefinitionHandle);
			}
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num * this.RowSize + this._EnclosingClassOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000211C7 File Offset: 0x0001F3C7
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._NestedClassOffset, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x04000881 RID: 2177
		internal readonly int NumberOfRows;

		// Token: 0x04000882 RID: 2178
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x04000883 RID: 2179
		private readonly int _NestedClassOffset;

		// Token: 0x04000884 RID: 2180
		private readonly int _EnclosingClassOffset;

		// Token: 0x04000885 RID: 2181
		internal readonly int RowSize;

		// Token: 0x04000886 RID: 2182
		internal readonly MemoryBlock Block;
	}
}
