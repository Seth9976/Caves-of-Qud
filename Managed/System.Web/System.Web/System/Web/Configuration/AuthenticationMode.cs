using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the authentication mode to use in a Web application.</summary>
	// Token: 0x02000558 RID: 1368
	public enum AuthenticationMode
	{
		/// <summary>Specifies no authentication.</summary>
		// Token: 0x04001FF7 RID: 8183
		None,
		/// <summary>Specifies Windows as the authentication mode. This mode applies when using the Internet Information Services (IIS) authentication methods Basic, Digest, Integrated Windows (NTLM/Kerberos), or certificates.</summary>
		// Token: 0x04001FF8 RID: 8184
		Windows,
		/// <summary>Specifies Microsoft Passport as the authentication mode.</summary>
		// Token: 0x04001FF9 RID: 8185
		[Obsolete("This field is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
		Passport,
		/// <summary>Specifies ASP.NET Forms-based authentication as the authentication mode.</summary>
		// Token: 0x04001FFA RID: 8186
		Forms
	}
}
