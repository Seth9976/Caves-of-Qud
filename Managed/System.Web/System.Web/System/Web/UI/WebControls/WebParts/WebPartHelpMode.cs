using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Specifies the available types of user interfaces (UIs) for displaying Help content for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	// Token: 0x02000475 RID: 1141
	public enum WebPartHelpMode
	{
		/// <summary>Opens a separate browser window, if the browser has this capability. A user must close the window before returning to the Web Parts page. </summary>
		// Token: 0x04001CF2 RID: 7410
		Modal,
		/// <summary>Opens a separate browser window, if the browser has this capability. A user does not have to close the window before returning to the Web page. </summary>
		// Token: 0x04001CF3 RID: 7411
		Modeless,
		/// <summary>Replaces the Web Parts page in the browser window.</summary>
		// Token: 0x04001CF4 RID: 7412
		Navigate
	}
}
