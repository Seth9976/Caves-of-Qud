using System;
using System.Collections;
using System.Diagnostics;

namespace System.Web.Util
{
	// Token: 0x0200014C RID: 332
	internal class WebTrace
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x0002859F File Offset: 0x0002679F
		[Conditional("WEBTRACE")]
		public static void PushContext(string context)
		{
			WebTrace.ctxStack.Push(context);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000285AC File Offset: 0x000267AC
		[Conditional("WEBTRACE")]
		public static void PopContext()
		{
			if (WebTrace.ctxStack.Count == 0)
			{
				return;
			}
			WebTrace.ctxStack.Pop();
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x000285C6 File Offset: 0x000267C6
		public static string Context
		{
			get
			{
				if (WebTrace.ctxStack.Count == 0)
				{
					return "No context";
				}
				return (string)WebTrace.ctxStack.Peek();
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x000285E9 File Offset: 0x000267E9
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x000285F0 File Offset: 0x000267F0
		public static bool StackTrace
		{
			get
			{
				return WebTrace.trace;
			}
			set
			{
				WebTrace.trace = value;
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg)
		{
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg, object arg)
		{
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg, object arg1, object arg2)
		{
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00003946 File Offset: 0x00001B46
		[Conditional("WEBTRACE")]
		public static void WriteLine(string msg, params object[] args)
		{
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000285F8 File Offset: 0x000267F8
		private static string Format(string msg)
		{
			if (WebTrace.trace)
			{
				return string.Format("{0}: {1}\n{2}", WebTrace.Context, msg, Environment.StackTrace);
			}
			return string.Format("{0}: {1}", WebTrace.Context, msg);
		}

		// Token: 0x0400121B RID: 4635
		private static Stack ctxStack = new Stack();

		// Token: 0x0400121C RID: 4636
		private static bool trace;
	}
}
