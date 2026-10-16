using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200014D RID: 333
	internal readonly struct AssemblyOSTableReader
	{
		// Token: 0x06000AEE RID: 2798 RVA: 0x00020898 File Offset: 0x0001EA98
		internal AssemblyOSTableReader(int numberOfRows, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._OSPlatformIdOffset = 0;
			this._OSMajorVersionIdOffset = this._OSPlatformIdOffset + 4;
			this._OSMinorVersionIdOffset = this._OSMajorVersionIdOffset + 4;
			this.RowSize = this._OSMinorVersionIdOffset + 4;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x04000843 RID: 2115
		internal readonly int NumberOfRows;

		// Token: 0x04000844 RID: 2116
		private readonly int _OSPlatformIdOffset;

		// Token: 0x04000845 RID: 2117
		private readonly int _OSMajorVersionIdOffset;

		// Token: 0x04000846 RID: 2118
		private readonly int _OSMinorVersionIdOffset;

		// Token: 0x04000847 RID: 2119
		internal readonly int RowSize;

		// Token: 0x04000848 RID: 2120
		internal readonly MemoryBlock Block;
	}
}
