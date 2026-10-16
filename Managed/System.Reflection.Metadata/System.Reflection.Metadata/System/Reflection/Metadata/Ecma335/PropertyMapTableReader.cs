using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000140 RID: 320
	internal readonly struct PropertyMapTableReader
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x0001FC04 File Offset: 0x0001DE04
		internal PropertyMapTableReader(int numberOfRows, int typeDefTableRowRefSize, int propertyRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = typeDefTableRowRefSize == 2;
			this._IsPropertyRefSizeSmall = propertyRefSize == 2;
			this._ParentOffset = 0;
			this._PropertyListOffset = this._ParentOffset + typeDefTableRowRefSize;
			this.RowSize = this._PropertyListOffset + propertyRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001FC68 File Offset: 0x0001DE68
		internal int FindPropertyMapRowIdFor(TypeDefinitionHandle typeDef)
		{
			int num = this.Block.LinearSearchReference(this.RowSize, this._ParentOffset, (uint)typeDef.RowId, this._IsTypeDefTableRowRefSizeSmall);
			return num + 1;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		internal TypeDefinitionHandle GetParentType(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._ParentOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		internal int GetPropertyListStartFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._PropertyListOffset, this._IsPropertyRefSizeSmall);
		}

		// Token: 0x040007EA RID: 2026
		internal readonly int NumberOfRows;

		// Token: 0x040007EB RID: 2027
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x040007EC RID: 2028
		private readonly bool _IsPropertyRefSizeSmall;

		// Token: 0x040007ED RID: 2029
		private readonly int _ParentOffset;

		// Token: 0x040007EE RID: 2030
		private readonly int _PropertyListOffset;

		// Token: 0x040007EF RID: 2031
		internal readonly int RowSize;

		// Token: 0x040007F0 RID: 2032
		internal readonly MemoryBlock Block;
	}
}
