using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000071 RID: 113
	internal static class StringUtils
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0000B59C File Offset: 0x0000979C
		internal static int IgnoreCaseMask(bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return 255;
			}
			return 32;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000B5A9 File Offset: 0x000097A9
		internal static bool IsEqualAscii(int a, int b, int ignoreCaseMask)
		{
			return a == b || ((a | 32) == (b | 32) && (a | ignoreCaseMask) - 97 <= 25);
		}
	}
}
