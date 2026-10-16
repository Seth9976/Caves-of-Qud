using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Indicates the page that the user will be directed to when he or she logs out of the Web site. </summary>
	// Token: 0x020002DF RID: 735
	public enum LogoutAction
	{
		/// <summary>Reloads the current page with the user logged out.</summary>
		// Token: 0x04001712 RID: 5906
		Refresh,
		/// <summary>Redirects the user to a specified URL.</summary>
		// Token: 0x04001713 RID: 5907
		Redirect,
		/// <summary>Redirects the user to the login page defined in the site's configuration files (Machine.config and Web.config).</summary>
		// Token: 0x04001714 RID: 5908
		RedirectToLoginPage
	}
}
