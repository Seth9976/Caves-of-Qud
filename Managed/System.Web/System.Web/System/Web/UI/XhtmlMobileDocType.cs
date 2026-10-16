using System;

namespace System.Web.UI
{
	/// <summary>Specifies the type of XHTML for the <see cref="T:System.Web.UI.XhtmlTextWriter" /> class to render to the page or control.</summary>
	// Token: 0x0200024D RID: 589
	public enum XhtmlMobileDocType
	{
		/// <summary>Specifies the XHTML Basic format. This format does not support frames and styles.</summary>
		// Token: 0x0400160E RID: 5646
		XhtmlBasic,
		/// <summary>Specifies the XHTML Mobile Profile format.</summary>
		// Token: 0x0400160F RID: 5647
		XhtmlMobileProfile,
		/// <summary>Specifies the WML 2.0 format.</summary>
		// Token: 0x04001610 RID: 5648
		Wml20
	}
}
