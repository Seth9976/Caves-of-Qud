using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200014F RID: 335
	internal readonly struct AssemblyRefProcessorTableReader
	{
		// Token: 0x06000AF6 RID: 2806 RVA: 0x00020B40 File Offset: 0x0001ED40
		internal AssemblyRefProcessorTableReader(int numberOfRows, int assemblyRefTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsAssemblyRefTableRowSizeSmall = assemblyRefTableRowRefSize == 2;
			this._ProcessorOffset = 0;
			this._AssemblyRefOffset = this._ProcessorOffset + 4;
			this.RowSize = this._AssemblyRefOffset + assemblyRefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x04000858 RID: 2136
		internal readonly int NumberOfRows;

		// Token: 0x04000859 RID: 2137
		private readonly bool _IsAssemblyRefTableRowSizeSmall;

		// Token: 0x0400085A RID: 2138
		private readonly int _ProcessorOffset;

		// Token: 0x0400085B RID: 2139
		private readonly int _AssemblyRefOffset;

		// Token: 0x0400085C RID: 2140
		internal readonly int RowSize;

		// Token: 0x0400085D RID: 2141
		internal readonly MemoryBlock Block;
	}
}
