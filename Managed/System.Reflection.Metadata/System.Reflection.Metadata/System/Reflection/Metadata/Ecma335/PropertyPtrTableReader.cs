using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000141 RID: 321
	internal readonly struct PropertyPtrTableReader
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x0001FD09 File Offset: 0x0001DF09
		internal PropertyPtrTableReader(int numberOfRows, int propertyTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsPropertyTableRowRefSizeSmall = propertyTableRowRefSize == 2;
			this._PropertyOffset = 0;
			this.RowSize = this._PropertyOffset + propertyTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001FD48 File Offset: 0x0001DF48
		internal PropertyDefinitionHandle GetPropertyFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return PropertyDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._PropertyOffset, this._IsPropertyTableRowRefSizeSmall));
		}

		// Token: 0x040007F1 RID: 2033
		internal readonly int NumberOfRows;

		// Token: 0x040007F2 RID: 2034
		private readonly bool _IsPropertyTableRowRefSizeSmall;

		// Token: 0x040007F3 RID: 2035
		private readonly int _PropertyOffset;

		// Token: 0x040007F4 RID: 2036
		internal readonly int RowSize;

		// Token: 0x040007F5 RID: 2037
		internal readonly MemoryBlock Block;
	}
}
