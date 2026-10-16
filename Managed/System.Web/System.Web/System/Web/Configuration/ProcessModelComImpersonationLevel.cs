using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the authentication level for COM security.</summary>
	// Token: 0x0200056B RID: 1387
	public enum ProcessModelComImpersonationLevel
	{
		/// <summary>Specifies that DCOM determines the impersonation level. This field is constant. </summary>
		// Token: 0x04002035 RID: 8245
		Default,
		/// <summary>Specifies that the client is anonymous to the server. This field is constant. </summary>
		// Token: 0x04002036 RID: 8246
		Anonymous,
		/// <summary>Specifies that the server process can impersonate the client's security context while acting on behalf of the client. This field is constant. </summary>
		// Token: 0x04002037 RID: 8247
		Delegate,
		/// <summary>Specifies that the server can obtain the client's identity. This field is constant. </summary>
		// Token: 0x04002038 RID: 8248
		Identify,
		/// <summary>Specifies that the server process can impersonate the client's security context while acting on behalf of the client. This field is constant. </summary>
		// Token: 0x04002039 RID: 8249
		Impersonate
	}
}
