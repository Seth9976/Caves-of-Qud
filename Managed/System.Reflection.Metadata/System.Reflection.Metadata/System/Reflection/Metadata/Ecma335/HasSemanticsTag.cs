using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000118 RID: 280
	internal static class HasSemanticsTag
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x0001D8C0 File Offset: 0x0001BAC0
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint hasSemantic)
		{
			uint num = 5908U >> (int)((int)(hasSemantic & 1U) << 3) << 24;
			uint num2 = hasSemantic >> 1;
			if ((num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001D8F9 File Offset: 0x0001BAF9
		internal static uint ConvertEventHandleToTag(EventDefinitionHandle eventDef)
		{
			return (uint)((eventDef.RowId << 1) | 0);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001D906 File Offset: 0x0001BB06
		internal static uint ConvertPropertyHandleToTag(PropertyDefinitionHandle propertyDef)
		{
			return (uint)((propertyDef.RowId << 1) | 1);
		}

		// Token: 0x0400065B RID: 1627
		internal const int NumberOfBits = 1;

		// Token: 0x0400065C RID: 1628
		internal const int LargeRowSize = 32768;

		// Token: 0x0400065D RID: 1629
		internal const uint Event = 0U;

		// Token: 0x0400065E RID: 1630
		internal const uint Property = 1U;

		// Token: 0x0400065F RID: 1631
		internal const uint TagMask = 1U;

		// Token: 0x04000660 RID: 1632
		internal const TableMask TablesReferenced = TableMask.Event | TableMask.Property;

		// Token: 0x04000661 RID: 1633
		internal const uint TagToTokenTypeByteVector = 5908U;
	}
}
