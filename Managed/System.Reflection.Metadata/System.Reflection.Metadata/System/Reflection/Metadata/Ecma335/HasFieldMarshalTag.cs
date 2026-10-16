using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000117 RID: 279
	internal static class HasFieldMarshalTag
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x0001D850 File Offset: 0x0001BA50
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint hasFieldMarshal)
		{
			uint num = 2052U >> (int)((int)(hasFieldMarshal & 1U) << 3) << 24;
			uint num2 = hasFieldMarshal >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001D889 File Offset: 0x0001BA89
		internal static uint ConvertToTag(EntityHandle handle)
		{
			if (handle.Type == 67108864U)
			{
				return (uint)((handle.RowId << 1) | 0);
			}
			if (handle.Type == 134217728U)
			{
				return (uint)((handle.RowId << 1) | 1);
			}
			return 0U;
		}

		// Token: 0x04000654 RID: 1620
		internal const int NumberOfBits = 1;

		// Token: 0x04000655 RID: 1621
		internal const int LargeRowSize = 32768;

		// Token: 0x04000656 RID: 1622
		internal const uint Field = 0U;

		// Token: 0x04000657 RID: 1623
		internal const uint Param = 1U;

		// Token: 0x04000658 RID: 1624
		internal const uint TagMask = 1U;

		// Token: 0x04000659 RID: 1625
		internal const TableMask TablesReferenced = TableMask.Field | TableMask.Param;

		// Token: 0x0400065A RID: 1626
		internal const uint TagToTokenTypeByteVector = 2052U;
	}
}
