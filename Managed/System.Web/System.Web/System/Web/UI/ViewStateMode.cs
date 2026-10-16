using System;

namespace System.Web.UI
{
	/// <summary>Specifies whether view state will be enabled for a control.</summary>
	// Token: 0x02000195 RID: 405
	public enum ViewStateMode
	{
		/// <summary>Inherit the value of <see cref="T:System.Web.UI.ViewStateMode" /> from the parent <see cref="T:System.Web.UI.Control" />.</summary>
		// Token: 0x0400133B RID: 4923
		Inherit,
		/// <summary>Enable view state for this control even if the parent control has view state disabled.</summary>
		// Token: 0x0400133C RID: 4924
		Enabled,
		/// <summary>Disable view state for this control even if the parent control has view state enabled.</summary>
		// Token: 0x0400133D RID: 4925
		Disabled
	}
}
