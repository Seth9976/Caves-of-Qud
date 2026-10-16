using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the path level of a Web-application configuration file.</summary>
	// Token: 0x02000577 RID: 1399
	public enum WebApplicationLevel
	{
		/// <summary>Specifies that the configuration file is in a global directory in relation to the current ASP.NET Web application.</summary>
		// Token: 0x0400206B RID: 8299
		AboveApplication = 10,
		/// <summary>Specifies that the configuration file is in the root directory of the current ASP.NET Web application.</summary>
		// Token: 0x0400206C RID: 8300
		AtApplication = 20,
		/// <summary>Specifies that the configuration file is in a sub-directory of the current ASP.NET Web application.</summary>
		// Token: 0x0400206D RID: 8301
		BelowApplication = 30
	}
}
