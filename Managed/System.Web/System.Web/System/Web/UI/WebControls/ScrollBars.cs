using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the visibility and position of scroll bars in a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
	// Token: 0x020002FF RID: 767
	[Flags]
	public enum ScrollBars
	{
		/// <summary>Displays no scroll bars.</summary>
		// Token: 0x0400174B RID: 5963
		None = 0,
		/// <summary>Displays only a horizontal scroll bar.</summary>
		// Token: 0x0400174C RID: 5964
		Horizontal = 1,
		/// <summary>Displays only a vertical scroll bar.</summary>
		// Token: 0x0400174D RID: 5965
		Vertical = 2,
		/// <summary>Displays both a horizontal and a vertical scroll bar.</summary>
		// Token: 0x0400174E RID: 5966
		Both = 3,
		/// <summary>Displays, horizontal, vertical, or both scroll bars as necessary. Otherwise, no scroll bars are shown.</summary>
		// Token: 0x0400174F RID: 5967
		Auto = 4
	}
}
