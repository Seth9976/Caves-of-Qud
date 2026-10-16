using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020009B2 RID: 2482
	internal class DefaultFilter : AssertFilter
	{
		// Token: 0x0600599A RID: 22938 RVA: 0x00132FD3 File Offset: 0x001311D3
		internal DefaultFilter()
		{
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x00132FDB File Offset: 0x001311DB
		[SecuritySafeCritical]
		public override AssertFilters AssertFailure(string condition, string message, StackTrace location, StackTrace.TraceFormat stackTraceFormat, string windowTitle)
		{
			return (AssertFilters)Assert.ShowDefaultAssertDialog(condition, message, location.ToString(stackTraceFormat), windowTitle);
		}
	}
}
