using System;

namespace Battlehub.UIControls
{
	// Token: 0x020003DB RID: 987
	public class ParentChangedEventArgs : EventArgs
	{
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x000B91FE File Offset: 0x000B73FE
		// (set) Token: 0x060024C4 RID: 9412 RVA: 0x000B9206 File Offset: 0x000B7406
		public TreeViewItem OldParent { get; private set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x000B920F File Offset: 0x000B740F
		// (set) Token: 0x060024C6 RID: 9414 RVA: 0x000B9217 File Offset: 0x000B7417
		public TreeViewItem NewParent { get; private set; }

		// Token: 0x060024C7 RID: 9415 RVA: 0x000B9220 File Offset: 0x000B7420
		public ParentChangedEventArgs(TreeViewItem oldParent, TreeViewItem newParent)
		{
			this.OldParent = oldParent;
			this.NewParent = newParent;
		}
	}
}
