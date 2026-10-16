using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000160 RID: 352
	internal static class TypeDefOrRefTag
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x00021658 File Offset: 0x0001F858
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint typeDefOrRefTag)
		{
			uint num = 1769730U >> (int)((int)(typeDefOrRefTag & 3U) << 3) << 24;
			uint num2 = typeDefOrRefTag >> 2;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x040008CD RID: 2253
		internal const int NumberOfBits = 2;

		// Token: 0x040008CE RID: 2254
		internal const int LargeRowSize = 16384;

		// Token: 0x040008CF RID: 2255
		internal const uint TypeDef = 0U;

		// Token: 0x040008D0 RID: 2256
		internal const uint TypeRef = 1U;

		// Token: 0x040008D1 RID: 2257
		internal const uint TypeSpec = 2U;

		// Token: 0x040008D2 RID: 2258
		internal const uint TagMask = 3U;

		// Token: 0x040008D3 RID: 2259
		internal const uint TagToTokenTypeByteVector = 1769730U;

		// Token: 0x040008D4 RID: 2260
		internal const TableMask TablesReferenced = TableMask.TypeRef | TableMask.TypeDef | TableMask.TypeSpec;
	}
}
