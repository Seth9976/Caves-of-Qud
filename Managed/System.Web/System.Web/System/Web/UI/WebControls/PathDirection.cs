using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Indicates the hierarchical order in which navigation nodes are rendered for site-navigation controls.</summary>
	// Token: 0x020002F8 RID: 760
	public enum PathDirection
	{
		/// <summary>Nodes are rendered in a hierarchical order from the top-most node to the current node, from left to right.</summary>
		// Token: 0x0400173D RID: 5949
		RootToCurrent,
		/// <summary>Nodes are rendered in a hierarchical order from the current node to the top-most node, from left to right.</summary>
		// Token: 0x0400173E RID: 5950
		CurrentToRoot
	}
}
