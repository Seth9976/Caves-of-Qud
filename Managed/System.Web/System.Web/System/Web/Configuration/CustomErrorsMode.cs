using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the values for the custom errors modality.</summary>
	// Token: 0x0200055C RID: 1372
	public enum CustomErrorsMode
	{
		/// <summary>Enables custom errors on remote clients only. Custom errors are shown only to remote clients and ASP.NET errors are shown to the local host.</summary>
		// Token: 0x04002005 RID: 8197
		RemoteOnly,
		/// <summary>Enables custom errors. If no <see cref="P:System.Web.Configuration.CustomErrorsSection.DefaultRedirect" /> is specified, standard errors are issued. </summary>
		// Token: 0x04002006 RID: 8198
		On,
		/// <summary>Disables custom errors, allowing display of standard errors.</summary>
		// Token: 0x04002007 RID: 8199
		Off
	}
}
