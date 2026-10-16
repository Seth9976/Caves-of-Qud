using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200013E RID: 318
	internal readonly struct EventPtrTableReader
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001FA71 File Offset: 0x0001DC71
		internal EventPtrTableReader(int numberOfRows, int eventTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsEventTableRowRefSizeSmall = eventTableRowRefSize == 2;
			this._EventOffset = 0;
			this.RowSize = this._EventOffset + eventTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001FAB0 File Offset: 0x0001DCB0
		internal EventDefinitionHandle GetEventFor(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return EventDefinitionHandle.FromRowId(this.Block.PeekReference(num + this._EventOffset, this._IsEventTableRowRefSizeSmall));
		}

		// Token: 0x040007DD RID: 2013
		internal readonly int NumberOfRows;

		// Token: 0x040007DE RID: 2014
		private readonly bool _IsEventTableRowRefSizeSmall;

		// Token: 0x040007DF RID: 2015
		private readonly int _EventOffset;

		// Token: 0x040007E0 RID: 2016
		internal readonly int RowSize;

		// Token: 0x040007E1 RID: 2017
		internal readonly MemoryBlock Block;
	}
}
