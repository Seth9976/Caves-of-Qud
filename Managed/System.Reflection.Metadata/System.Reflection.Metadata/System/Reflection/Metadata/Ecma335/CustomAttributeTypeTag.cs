using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000113 RID: 275
	internal static class CustomAttributeTypeTag
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x0001D508 File Offset: 0x0001B708
		[MethodImpl(256)]
		internal static EntityHandle ConvertToHandle(uint customAttributeType)
		{
			uint num = (uint)(168165376UL >> (int)((int)(customAttributeType & 7U) << 3)) << 24;
			uint num2 = customAttributeType >> 3;
			if (num == 0U || (num2 & 4278190080U) != 0U)
			{
				Throw.InvalidCodedIndex();
			}
			return new EntityHandle(num | num2);
		}

		// Token: 0x04000621 RID: 1569
		internal const int NumberOfBits = 3;

		// Token: 0x04000622 RID: 1570
		internal const int LargeRowSize = 8192;

		// Token: 0x04000623 RID: 1571
		internal const uint MethodDef = 2U;

		// Token: 0x04000624 RID: 1572
		internal const uint MemberRef = 3U;

		// Token: 0x04000625 RID: 1573
		internal const uint TagMask = 7U;

		// Token: 0x04000626 RID: 1574
		internal const ulong TagToTokenTypeByteVector = 168165376UL;

		// Token: 0x04000627 RID: 1575
		internal const TableMask TablesReferenced = TableMask.MethodDef | TableMask.MemberRef;
	}
}
