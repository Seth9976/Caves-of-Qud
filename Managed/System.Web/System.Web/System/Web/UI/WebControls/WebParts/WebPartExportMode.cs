using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Specifies whether all, some, or none of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control's properties can be exported.</summary>
	// Token: 0x02000474 RID: 1140
	public enum WebPartExportMode
	{
		/// <summary>None of a Web Parts control's properties can be exported. </summary>
		// Token: 0x04001CEE RID: 7406
		None,
		/// <summary>All of a Web Parts control's properties can be exported.</summary>
		// Token: 0x04001CEF RID: 7407
		All,
		/// <summary>Only properties of a Web Parts control that have been defined as non-sensitive can be exported.  </summary>
		// Token: 0x04001CF0 RID: 7408
		NonSensitiveData
	}
}
