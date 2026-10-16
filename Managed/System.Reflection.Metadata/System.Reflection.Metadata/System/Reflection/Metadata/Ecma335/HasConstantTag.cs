using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000114 RID: 276
	internal static class HasConstantTag
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x0001D548 File Offset: 0x0001B748
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint hasConstant)
		{
			uint num = 1509380U >> (int)((int)(hasConstant & 3U) << 3) << 24;
			uint num2 = hasConstant >> 2;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001D584 File Offset: 0x0001B784
		internal static uint ConvertToTag(EntityHandle token)
		{
			HandleKind kind = token.Kind;
			uint rowId = (uint)token.RowId;
			if (kind == HandleKind.FieldDefinition)
			{
				return (rowId << 2) | 0U;
			}
			if (kind == HandleKind.Parameter)
			{
				return (rowId << 2) | 1U;
			}
			if (kind == HandleKind.PropertyDefinition)
			{
				return (rowId << 2) | 2U;
			}
			return 0U;
		}

		// Token: 0x04000628 RID: 1576
		internal const int NumberOfBits = 2;

		// Token: 0x04000629 RID: 1577
		internal const int LargeRowSize = 16384;

		// Token: 0x0400062A RID: 1578
		internal const uint Field = 0U;

		// Token: 0x0400062B RID: 1579
		internal const uint Param = 1U;

		// Token: 0x0400062C RID: 1580
		internal const uint Property = 2U;

		// Token: 0x0400062D RID: 1581
		internal const uint TagMask = 3U;

		// Token: 0x0400062E RID: 1582
		internal const TableMask TablesReferenced = TableMask.Field | TableMask.Param | TableMask.Property;

		// Token: 0x0400062F RID: 1583
		internal const uint TagToTokenTypeByteVector = 1509380U;
	}
}
