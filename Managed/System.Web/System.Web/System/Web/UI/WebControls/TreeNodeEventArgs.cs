using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCheckChanged" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCollapsed" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeDataBound" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" />, and <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodePopulate" /> events of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000319 RID: 793
	public sealed class TreeNodeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> class using the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</summary>
		/// <param name="node">A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the current node when the event is raised. </param>
		// Token: 0x06001BDA RID: 7130 RVA: 0x000438FD File Offset: 0x00041AFD
		public TreeNodeEventArgs(TreeNode node)
		{
			this._node = node;
		}

		/// <summary>Gets the node that raised the event.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the node that raised the event.</returns>
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0004390C File Offset: 0x00041B0C
		public TreeNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0400178D RID: 6029
		private TreeNode _node;
	}
}
