using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000122 RID: 290
	internal static class HeapHandleType
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x0001DA71 File Offset: 0x0001BC71
		internal static bool IsValidHeapOffset(uint offset)
		{
			return (offset & 3758096384U) == 0U;
		}

		// Token: 0x040006CC RID: 1740
		internal const int OffsetBitCount = 29;

		// Token: 0x040006CD RID: 1741
		internal const uint OffsetMask = 536870911U;

		// Token: 0x040006CE RID: 1742
		internal const uint VirtualBit = 2147483648U;
	}
}
