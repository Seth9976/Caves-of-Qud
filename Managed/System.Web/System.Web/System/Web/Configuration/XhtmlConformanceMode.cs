using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the authentication mode to use in a Web application.</summary>
	// Token: 0x02000579 RID: 1401
	public enum XhtmlConformanceMode
	{
		/// <summary>Reverts a number of rendering changes made for conformance to the v1.1 rendering behavior. </summary>
		// Token: 0x04002072 RID: 8306
		Transitional,
		/// <summary>XHTML 1.0 Transitional </summary>
		// Token: 0x04002073 RID: 8307
		Legacy,
		/// <summary>XHTML 1.0 Strict conformance </summary>
		// Token: 0x04002074 RID: 8308
		Strict
	}
}
