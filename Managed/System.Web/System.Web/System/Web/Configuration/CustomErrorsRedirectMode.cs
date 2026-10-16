using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies values for how the URL of the original request is handled when a custom error page is displayed.</summary>
	// Token: 0x0200055D RID: 1373
	public enum CustomErrorsRedirectMode
	{
		/// <summary>Display the error page and change the URL of the original request.</summary>
		// Token: 0x04002009 RID: 8201
		ResponseRedirect,
		/// <summary>Display the error page without changing the original URL.</summary>
		// Token: 0x0400200A RID: 8202
		ResponseRewrite
	}
}
