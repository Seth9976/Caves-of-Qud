using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200084B RID: 2123
	internal static class JitHelpers
	{
		// Token: 0x060046CD RID: 18125 RVA: 0x000E7235 File Offset: 0x000E5435
		internal static T UnsafeCast<T>(object o) where T : class
		{
			return Array.UnsafeMov<object, T>(o);
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x000E723D File Offset: 0x000E543D
		internal static int UnsafeEnumCast<T>(T val) where T : struct
		{
			return Array.UnsafeMov<T, int>(val);
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x000E7245 File Offset: 0x000E5445
		internal static long UnsafeEnumCastLong<T>(T val) where T : struct
		{
			return Array.UnsafeMov<T, long>(val);
		}
	}
}
