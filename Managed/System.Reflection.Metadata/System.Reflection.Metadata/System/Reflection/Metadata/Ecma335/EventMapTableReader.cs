using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200013D RID: 317
	internal readonly struct EventMapTableReader
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001F96C File Offset: 0x0001DB6C
		internal EventMapTableReader(int numberOfRows, int typeDefTableRowRefSize, int eventRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsTypeDefTableRowRefSizeSmall = typeDefTableRowRefSize == 2;
			this._IsEventRefSizeSmall = eventRefSize == 2;
			this._ParentOffset = 0;
			this._EventListOffset = this._ParentOffset + typeDefTableRowRefSize;
			this.RowSize = this._EventListOffset + eventRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		internal int FindEventMapRowIdFor(TypeDefinitionHandle typeDef)
		{
			int num = this.Block.LinearSearchReference(this.RowSize, this._ParentOffset, (uint)typeDef.RowId, this._IsTypeDefTableRowRefSizeSmall);
			return num + 1;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001FA08 File Offset: 0x0001DC08
		internal TypeDefinitionHandle GetParentType(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return TypeDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._ParentOffset, this._IsTypeDefTableRowRefSizeSmall));
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001FA40 File Offset: 0x0001DC40
		internal int GetEventListStartFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return this.Block.PeekReference(num + this._EventListOffset, this._IsEventRefSizeSmall);
		}

		// Token: 0x040007D6 RID: 2006
		internal readonly int NumberOfRows;

		// Token: 0x040007D7 RID: 2007
		private readonly bool _IsTypeDefTableRowRefSizeSmall;

		// Token: 0x040007D8 RID: 2008
		private readonly bool _IsEventRefSizeSmall;

		// Token: 0x040007D9 RID: 2009
		private readonly int _ParentOffset;

		// Token: 0x040007DA RID: 2010
		private readonly int _EventListOffset;

		// Token: 0x040007DB RID: 2011
		internal readonly int RowSize;

		// Token: 0x040007DC RID: 2012
		internal readonly MemoryBlock Block;
	}
}
