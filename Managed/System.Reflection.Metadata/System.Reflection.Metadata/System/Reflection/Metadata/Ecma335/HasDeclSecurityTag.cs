using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000116 RID: 278
	internal static class HasDeclSecurityTag
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint hasDeclSecurity)
		{
			uint num = 2098690U >> (int)((int)(hasDeclSecurity & 3U) << 3) << 24;
			uint num2 = hasDeclSecurity >> 2;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001D804 File Offset: 0x0001BA04
		internal static uint ConvertToTag(EntityHandle handle)
		{
			uint type = handle.Type;
			uint rowId = (uint)handle.RowId;
			uint num = type >> 24;
			uint num2;
			if (num != 2U)
			{
				if (num != 6U)
				{
					if (num != 32U)
					{
						num2 = 0U;
					}
					else
					{
						num2 = (rowId << 2) | 2U;
					}
				}
				else
				{
					num2 = (rowId << 2) | 1U;
				}
			}
			else
			{
				num2 = (rowId << 2) | 0U;
			}
			return num2;
		}

		// Token: 0x0400064C RID: 1612
		internal const int NumberOfBits = 2;

		// Token: 0x0400064D RID: 1613
		internal const int LargeRowSize = 16384;

		// Token: 0x0400064E RID: 1614
		internal const uint TypeDef = 0U;

		// Token: 0x0400064F RID: 1615
		internal const uint MethodDef = 1U;

		// Token: 0x04000650 RID: 1616
		internal const uint Assembly = 2U;

		// Token: 0x04000651 RID: 1617
		internal const uint TagMask = 3U;

		// Token: 0x04000652 RID: 1618
		internal const TableMask TablesReferenced = TableMask.TypeDef | TableMask.MethodDef | TableMask.Assembly;

		// Token: 0x04000653 RID: 1619
		internal const uint TagToTokenTypeByteVector = 2098690U;
	}
}
