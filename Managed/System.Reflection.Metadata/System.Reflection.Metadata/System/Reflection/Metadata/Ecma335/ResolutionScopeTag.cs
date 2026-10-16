using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000129 RID: 297
	internal static class ResolutionScopeTag
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x0001E21C File Offset: 0x0001C41C
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint resolutionScope)
		{
			uint num = 19077632U >> (int)((int)(resolutionScope & 3U) << 3) << 24;
			uint num2 = resolutionScope >> 2;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0400073E RID: 1854
		internal const int NumberOfBits = 2;

		// Token: 0x0400073F RID: 1855
		internal const int LargeRowSize = 16384;

		// Token: 0x04000740 RID: 1856
		internal const uint Module = 0U;

		// Token: 0x04000741 RID: 1857
		internal const uint ModuleRef = 1U;

		// Token: 0x04000742 RID: 1858
		internal const uint AssemblyRef = 2U;

		// Token: 0x04000743 RID: 1859
		internal const uint TypeRef = 3U;

		// Token: 0x04000744 RID: 1860
		internal const uint TagMask = 3U;

		// Token: 0x04000745 RID: 1861
		internal const uint TagToTokenTypeByteVector = 19077632U;

		// Token: 0x04000746 RID: 1862
		internal const TableMask TablesReferenced = TableMask.Module | TableMask.TypeRef | TableMask.ModuleRef | TableMask.AssemblyRef;
	}
}
