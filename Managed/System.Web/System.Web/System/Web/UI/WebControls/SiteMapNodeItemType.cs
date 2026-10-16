using System;

namespace System.Web.UI.WebControls
{
	/// <summary>The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" /> enumeration is used by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control to identify the type of a <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> node within a node hierarchy.</summary>
	// Token: 0x02000305 RID: 773
	public enum SiteMapNodeItemType
	{
		/// <summary>The top node of the site navigation hierarchy. There can be only one root node.</summary>
		// Token: 0x04001754 RID: 5972
		Root,
		/// <summary>A parent node of the currently viewed page in the site navigation path. A parent node is any node that is found between the root node and the current node in the navigation hierarchy.</summary>
		// Token: 0x04001755 RID: 5973
		Parent,
		/// <summary>The currently viewed page in the site navigation path.</summary>
		// Token: 0x04001756 RID: 5974
		Current,
		/// <summary>A site map navigation path separator. The default separator for the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control is the "&gt;" character.</summary>
		// Token: 0x04001757 RID: 5975
		PathSeparator
	}
}
