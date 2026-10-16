using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the event or events to raise when a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control is selected.</summary>
	// Token: 0x0200031B RID: 795
	public enum TreeNodeSelectAction
	{
		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.SelectedNodeChanged" /> event when a node is selected.</summary>
		// Token: 0x0400178F RID: 6031
		Select,
		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" /> event when a node is selected.</summary>
		// Token: 0x04001790 RID: 6032
		Expand,
		/// <summary>Raises both the <see cref="E:System.Web.UI.WebControls.TreeView.SelectedNodeChanged" /> and <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" /> events when a node is selected.</summary>
		// Token: 0x04001791 RID: 6033
		SelectExpand,
		/// <summary>Raises no events when a node is selected.</summary>
		// Token: 0x04001792 RID: 6034
		None
	}
}
