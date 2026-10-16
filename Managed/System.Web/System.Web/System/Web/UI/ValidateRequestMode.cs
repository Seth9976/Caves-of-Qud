using System;

namespace System.Web.UI
{
	/// <summary>Specifies the type of request validation for a control.</summary>
	// Token: 0x02000193 RID: 403
	public enum ValidateRequestMode
	{
		/// <summary>Request validation uses the same behavior as its parent control.</summary>
		// Token: 0x04001333 RID: 4915
		Inherit,
		/// <summary>Request validation is disabled.</summary>
		// Token: 0x04001334 RID: 4916
		Disabled,
		/// <summary>Request validation is enabled.</summary>
		// Token: 0x04001335 RID: 4917
		Enabled
	}
}
