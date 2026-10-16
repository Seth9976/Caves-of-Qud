using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web.Util
{
	// Token: 0x02000110 RID: 272
	internal static class Debug
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message)
		{
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, bool includePrefix)
		{
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e)
		{
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Trace(string tagName, Exception e)
		{
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e, bool includePrefix)
		{
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		public static void TraceException(string tagName, Exception e)
		{
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Assert(bool assertion, string message)
		{
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Assert(bool assertion)
		{
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Fail(string message)
		{
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00008A4D File Offset: 0x00006C4D
		internal static bool IsTagEnabled(string tagName)
		{
			return false;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00008A4D File Offset: 0x00006C4D
		internal static bool IsTagPresent(string tagName)
		{
			return false;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000255C4 File Offset: 0x000237C4
		internal static bool IsDebuggerPresent()
		{
			return Debug.NativeMethods.IsDebuggerPresent() || Debugger.IsAttached;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Break()
		{
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void AlwaysValidate(string tagName)
		{
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void CheckValid(bool assertion, string message)
		{
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Validate(object obj)
		{
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void ValidateArrayBounds<T>(T[] array, int offset, int count)
		{
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Validate(string tagName, object obj)
		{
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("DBG")]
		internal static void Dump(string tagName, object obj)
		{
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0000EE63 File Offset: 0x0000D063
		internal static string FormatUtcDate(DateTime utcTime)
		{
			return string.Empty;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0000EE63 File Offset: 0x0000D063
		internal static string FormatLocalDate(DateTime localTime)
		{
			return string.Empty;
		}

		// Token: 0x040011A9 RID: 4521
		internal const string TAG_INTERNAL = "Internal";

		// Token: 0x040011AA RID: 4522
		internal const string TAG_EXTERNAL = "External";

		// Token: 0x040011AB RID: 4523
		internal const string TAG_ALL = "*";

		// Token: 0x040011AC RID: 4524
		internal const string DATE_FORMAT = "yyyy/MM/dd HH:mm:ss.ffff";

		// Token: 0x040011AD RID: 4525
		internal const string TIME_FORMAT = "HH:mm:ss:ffff";

		// Token: 0x02000111 RID: 273
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{
			// Token: 0x06000DD8 RID: 3544
			[DllImport("kernel32.dll")]
			internal static extern bool IsDebuggerPresent();
		}
	}
}
