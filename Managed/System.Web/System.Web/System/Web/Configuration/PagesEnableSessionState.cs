using System;

namespace System.Web.Configuration
{
	/// <summary>Used to determine session-state activation for a single Web page or an entire Web application.</summary>
	// Token: 0x02000569 RID: 1385
	public enum PagesEnableSessionState
	{
		/// <summary>Session state is disabled.</summary>
		// Token: 0x04002029 RID: 8233
		False,
		/// <summary>Session state is enabled, but not writable.</summary>
		// Token: 0x0400202A RID: 8234
		ReadOnly,
		/// <summary>Session state is enabled.</summary>
		// Token: 0x0400202B RID: 8235
		True
	}
}
