using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000161 RID: 353
	internal static class TypeOrMethodDefTag
	{
		// Token: 0x06000B20 RID: 2848 RVA: 0x00021694 File Offset: 0x0001F894
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint typeOrMethodDef)
		{
			uint num = 1538U >> (int)((int)(typeOrMethodDef & 1U) << 3) << 24;
			uint num2 = typeOrMethodDef >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000216CD File Offset: 0x0001F8CD
		internal static uint ConvertTypeDefRowIdToTag(TypeDefinitionHandle typeDef)
		{
			return (uint)((typeDef.RowId << 1) | 0);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000216DA File Offset: 0x0001F8DA
		internal static uint ConvertMethodDefToTag(MethodDefinitionHandle methodDef)
		{
			return (uint)((methodDef.RowId << 1) | 1);
		}

		// Token: 0x040008D5 RID: 2261
		internal const int NumberOfBits = 1;

		// Token: 0x040008D6 RID: 2262
		internal const int LargeRowSize = 32768;

		// Token: 0x040008D7 RID: 2263
		internal const uint TypeDef = 0U;

		// Token: 0x040008D8 RID: 2264
		internal const uint MethodDef = 1U;

		// Token: 0x040008D9 RID: 2265
		internal const uint TagMask = 1U;

		// Token: 0x040008DA RID: 2266
		internal const uint TagToTokenTypeByteVector = 1538U;

		// Token: 0x040008DB RID: 2267
		internal const TableMask TablesReferenced = TableMask.TypeDef | TableMask.MethodDef;
	}
}
