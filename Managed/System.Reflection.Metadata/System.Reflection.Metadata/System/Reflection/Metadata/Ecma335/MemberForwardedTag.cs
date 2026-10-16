using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x0200011B RID: 283
	internal static class MemberForwardedTag
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x0001D9E8 File Offset: 0x0001BBE8
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint memberForwarded)
		{
			uint num = 1540U >> (int)((int)(memberForwarded & 1U) << 3) << 24;
			uint num2 = memberForwarded >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001DA21 File Offset: 0x0001BC21
		internal static uint ConvertMethodDefToTag(MethodDefinitionHandle methodDef)
		{
			return (uint)((methodDef.RowId << 1) | 1);
		}

		// Token: 0x0400066B RID: 1643
		internal const int NumberOfBits = 1;

		// Token: 0x0400066C RID: 1644
		internal const int LargeRowSize = 32768;

		// Token: 0x0400066D RID: 1645
		internal const uint Field = 0U;

		// Token: 0x0400066E RID: 1646
		internal const uint MethodDef = 1U;

		// Token: 0x0400066F RID: 1647
		internal const uint TagMask = 1U;

		// Token: 0x04000670 RID: 1648
		internal const TableMask TablesReferenced = TableMask.Field | TableMask.MethodDef;

		// Token: 0x04000671 RID: 1649
		internal const uint TagToTokenTypeByteVector = 1540U;
	}
}
