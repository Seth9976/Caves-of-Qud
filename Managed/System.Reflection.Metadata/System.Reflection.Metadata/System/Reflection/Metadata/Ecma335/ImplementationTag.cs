using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011A RID: 282
	internal static class ImplementationTag
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x0001D9AC File Offset: 0x0001BBAC
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint implementation)
		{
			uint num = 2564902U >> (int)((int)(implementation & 3U) << 3) << 24;
			uint num2 = implementation >> 2;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x04000663 RID: 1635
		internal const int NumberOfBits = 2;

		// Token: 0x04000664 RID: 1636
		internal const int LargeRowSize = 16384;

		// Token: 0x04000665 RID: 1637
		internal const uint File = 0U;

		// Token: 0x04000666 RID: 1638
		internal const uint AssemblyRef = 1U;

		// Token: 0x04000667 RID: 1639
		internal const uint ExportedType = 2U;

		// Token: 0x04000668 RID: 1640
		internal const uint TagMask = 3U;

		// Token: 0x04000669 RID: 1641
		internal const uint TagToTokenTypeByteVector = 2564902U;

		// Token: 0x0400066A RID: 1642
		internal const TableMask TablesReferenced = TableMask.AssemblyRef | TableMask.File | TableMask.ExportedType;
	}
}
