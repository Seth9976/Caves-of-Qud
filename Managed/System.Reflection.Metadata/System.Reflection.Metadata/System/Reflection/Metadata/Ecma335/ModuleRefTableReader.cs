using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000145 RID: 325
	internal readonly struct ModuleRefTableReader
	{
		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002020F File Offset: 0x0001E40F
		internal ModuleRefTableReader(int numberOfRows, int stringHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsStringHeapRefSizeSmall = stringHeapRefSize == 2;
			this._NameOffset = 0;
			this.RowSize = this._NameOffset + stringHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00020250 File Offset: 0x0001E450
		internal StringHandle GetName(ModuleReferenceHandle handle)
		{
			int num = (handle.RowId - 1) * this.RowSize;
			return StringHandle.FromOffset(this.Block.PeekHeapReference(num + this._NameOffset, this._IsStringHeapRefSizeSmall));
		}

		// Token: 0x0400080E RID: 2062
		internal readonly int NumberOfRows;

		// Token: 0x0400080F RID: 2063
		private readonly bool _IsStringHeapRefSizeSmall;

		// Token: 0x04000810 RID: 2064
		private readonly int _NameOffset;

		// Token: 0x04000811 RID: 2065
		internal readonly int RowSize;

		// Token: 0x04000812 RID: 2066
		internal readonly MemoryBlock Block;
	}
}
