using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200014C RID: 332
	internal readonly struct AssemblyProcessorTableReader
	{
		// Token: 0x06000AED RID: 2797 RVA: 0x00020863 File Offset: 0x0001EA63
		internal AssemblyProcessorTableReader(int numberOfRows, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._ProcessorOffset = 0;
			this.RowSize = this._ProcessorOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x0400083F RID: 2111
		internal readonly int NumberOfRows;

		// Token: 0x04000840 RID: 2112
		private readonly int _ProcessorOffset;

		// Token: 0x04000841 RID: 2113
		internal readonly int RowSize;

		// Token: 0x04000842 RID: 2114
		internal readonly MemoryBlock Block;
	}
}
