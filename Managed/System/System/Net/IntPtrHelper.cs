using System;

namespace System.Net
{
	// Token: 0x020005DE RID: 1502
	internal static class IntPtrHelper
	{
		// Token: 0x0600303F RID: 12351 RVA: 0x000A6454 File Offset: 0x000A4654
		internal static IntPtr Add(IntPtr a, int b)
		{
			return (IntPtr)((long)a + (long)b);
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000A6464 File Offset: 0x000A4664
		internal static long Subtract(IntPtr a, IntPtr b)
		{
			return (long)a - (long)b;
		}
	}
}
