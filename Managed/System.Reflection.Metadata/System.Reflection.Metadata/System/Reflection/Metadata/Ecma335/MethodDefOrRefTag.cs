using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000126 RID: 294
	internal static class MethodDefOrRefTag
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x0001DABC File Offset: 0x0001BCBC
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint methodDefOrRef)
		{
			uint num = 2566U >> (int)((int)(methodDefOrRef & 1U) << 3) << 24;
			uint num2 = methodDefOrRef >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x0400072C RID: 1836
		internal const int NumberOfBits = 1;

		// Token: 0x0400072D RID: 1837
		internal const int LargeRowSize = 32768;

		// Token: 0x0400072E RID: 1838
		internal const uint MethodDef = 0U;

		// Token: 0x0400072F RID: 1839
		internal const uint MemberRef = 1U;

		// Token: 0x04000730 RID: 1840
		internal const uint TagMask = 1U;

		// Token: 0x04000731 RID: 1841
		internal const TableMask TablesReferenced = TableMask.MethodDef | TableMask.MemberRef;

		// Token: 0x04000732 RID: 1842
		internal const uint TagToTokenTypeByteVector = 2566U;
	}
}
