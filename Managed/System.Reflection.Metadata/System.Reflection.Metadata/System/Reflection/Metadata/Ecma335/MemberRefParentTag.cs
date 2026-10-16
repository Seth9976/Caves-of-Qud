using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011C RID: 284
	internal static class MemberRefParentTag
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x0001DA30 File Offset: 0x0001BC30
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint memberRef)
		{
			uint num = (uint)((uint)(116066484482UL >> (int)((int)(memberRef & 7U) << 3)) << 24);
			uint num2 = memberRef >> 3;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x04000672 RID: 1650
		internal const int NumberOfBits = 3;

		// Token: 0x04000673 RID: 1651
		internal const int LargeRowSize = 8192;

		// Token: 0x04000674 RID: 1652
		internal const uint TypeDef = 0U;

		// Token: 0x04000675 RID: 1653
		internal const uint TypeRef = 1U;

		// Token: 0x04000676 RID: 1654
		internal const uint ModuleRef = 2U;

		// Token: 0x04000677 RID: 1655
		internal const uint MethodDef = 3U;

		// Token: 0x04000678 RID: 1656
		internal const uint TypeSpec = 4U;

		// Token: 0x04000679 RID: 1657
		internal const uint TagMask = 7U;

		// Token: 0x0400067A RID: 1658
		internal const TableMask TablesReferenced = TableMask.TypeRef | TableMask.TypeDef | TableMask.MethodDef | TableMask.ModuleRef | TableMask.TypeSpec;

		// Token: 0x0400067B RID: 1659
		internal const ulong TagToTokenTypeByteVector = 116066484482UL;
	}
}
