using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000150 RID: 336
	internal readonly struct AssemblyRefOSTableReader
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x00020B98 File Offset: 0x0001ED98
		internal AssemblyRefOSTableReader(int numberOfRows, int assemblyRefTableRowRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsAssemblyRefTableRowRefSizeSmall = assemblyRefTableRowRefSize == 2;
			this._OSPlatformIdOffset = 0;
			this._OSMajorVersionIdOffset = this._OSPlatformIdOffset + 4;
			this._OSMinorVersionIdOffset = this._OSMajorVersionIdOffset + 4;
			this._AssemblyRefOffset = this._OSMinorVersionIdOffset + 4;
			this.RowSize = this._AssemblyRefOffset + assemblyRefTableRowRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
		}

		// Token: 0x0400085E RID: 2142
		internal readonly int NumberOfRows;

		// Token: 0x0400085F RID: 2143
		private readonly bool _IsAssemblyRefTableRowRefSizeSmall;

		// Token: 0x04000860 RID: 2144
		private readonly int _OSPlatformIdOffset;

		// Token: 0x04000861 RID: 2145
		private readonly int _OSMajorVersionIdOffset;

		// Token: 0x04000862 RID: 2146
		private readonly int _OSMinorVersionIdOffset;

		// Token: 0x04000863 RID: 2147
		private readonly int _AssemblyRefOffset;

		// Token: 0x04000864 RID: 2148
		internal readonly int RowSize;

		// Token: 0x04000865 RID: 2149
		internal readonly MemoryBlock Block;
	}
}
