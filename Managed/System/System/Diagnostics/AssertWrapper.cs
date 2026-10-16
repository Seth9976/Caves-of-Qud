using System;

namespace System.Diagnostics
{
	// Token: 0x0200024B RID: 587
	internal class AssertWrapper
	{
		// Token: 0x06001217 RID: 4631 RVA: 0x0004E25E File Offset: 0x0004C45E
		public static void ShowAssert(string stackTrace, StackFrame frame, string message, string detailMessage)
		{
			new DefaultTraceListener().Fail(message, detailMessage);
		}
	}
}
