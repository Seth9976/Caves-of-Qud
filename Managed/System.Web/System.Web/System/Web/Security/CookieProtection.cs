using System;

namespace System.Web.Security
{
	/// <summary>Describes how information in a cookie is protected.</summary>
	// Token: 0x020004B5 RID: 1205
	public enum CookieProtection
	{
		/// <summary>Do not protect information in the cookie. Information in the cookie is stored in clear text and not validated when sent back to the server.</summary>
		// Token: 0x04001DAB RID: 7595
		None,
		/// <summary>Ensure that the information in the cookie has not been altered before being sent back to the server.</summary>
		// Token: 0x04001DAC RID: 7596
		Validation,
		/// <summary>Encrypt the information in the cookie.</summary>
		// Token: 0x04001DAD RID: 7597
		Encryption,
		/// <summary>Use both <see cref="F:System.Web.Security.CookieProtection.Validation" /> and <see cref="F:System.Web.Security.CookieProtection.Encryption" /> to protect the information in the cookie.</summary>
		// Token: 0x04001DAE RID: 7598
		All
	}
}
