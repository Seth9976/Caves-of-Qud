using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200000E RID: 14
	internal static class NumberFormatInfoEx
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00007FA4 File Offset: 0x000061A4
		[NullableContext(1)]
		internal static bool HasInvariantNumberSigns(this NumberFormatInfo info)
		{
			return info.PositiveSign == "+" && info.NegativeSign == "-";
		}
	}
}
