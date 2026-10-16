using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200013A RID: 314
	internal struct ClassLayoutTableReader
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001F678 File Offset: 0x0001D878
		internal ClassLayoutTableReader(int numberOfRows, bool declaredSorted, int typeDefTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = typeDefTableRowRefSize == 2;
			this._PackagingSizeOffset = 0;
			this._ClassSizeOffset = this._PackagingSizeOffset + 2;
			this._ParentOffset = this._ClassSizeOffset + 4;
			this.RowSize = this._ParentOffset + typeDefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.ClassLayout);
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
		internal TypeDefinitionHandle GetParent(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._ParentOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001F728 File Offset: 0x0001D928
		internal ushort GetPackingSize(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt16(num + this._PackagingSizeOffset);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001F754 File Offset: 0x0001D954
		internal uint GetClassSize(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekUInt32(num + this._ClassSizeOffset);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001F77F File Offset: 0x0001D97F
		internal int FindRow(TypeDefinitionHandle typeDef)
		{
			return 1 + this.Block.BinarySearchReference(this.NumberOfRows, this.RowSize, this._ParentOffset, (uint)typeDef.RowId, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001F7AD File Offset: 0x0001D9AD
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsTypeDefTableRowRefSizeSmall);
		}

		// Token: 0x040007C4 RID: 1988
		internal int NumberOfRows;

		// Token: 0x040007C5 RID: 1989
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x040007C6 RID: 1990
		private readonly int _PackagingSizeOffset;

		// Token: 0x040007C7 RID: 1991
		private readonly int _ClassSizeOffset;

		// Token: 0x040007C8 RID: 1992
		private readonly int _ParentOffset;

		// Token: 0x040007C9 RID: 1993
		internal readonly int RowSize;

		// Token: 0x040007CA RID: 1994
		internal MemoryBlock Block;
	}
}
