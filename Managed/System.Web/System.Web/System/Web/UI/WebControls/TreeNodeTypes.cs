using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the different node types (leaf, parent, and root) in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	// Token: 0x0200031C RID: 796
	[Flags]
	public enum TreeNodeTypes
	{
		/// <summary>No nodes.</summary>
		// Token: 0x04001794 RID: 6036
		None = 0,
		/// <summary>A node with no parent node and one or more child nodes.</summary>
		// Token: 0x04001795 RID: 6037
		Root = 1,
		/// <summary>A node with a parent node and one or more child nodes.</summary>
		// Token: 0x04001796 RID: 6038
		Parent = 2,
		/// <summary>A node with no child nodes.</summary>
		// Token: 0x04001797 RID: 6039
		Leaf = 4,
		/// <summary>All nodes.</summary>
		// Token: 0x04001798 RID: 6040
		All = 7
	}
}
