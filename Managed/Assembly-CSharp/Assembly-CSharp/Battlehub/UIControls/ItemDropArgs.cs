using System;

namespace Battlehub.UIControls
{
	// Token: 0x020003CB RID: 971
	public class ItemDropArgs : EventArgs
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x000B59D0 File Offset: 0x000B3BD0
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x000B59D8 File Offset: 0x000B3BD8
		public object[] DragItems { get; private set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x000B59E1 File Offset: 0x000B3BE1
		// (set) Token: 0x06002401 RID: 9217 RVA: 0x000B59E9 File Offset: 0x000B3BE9
		public object DropTarget { get; private set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x000B59F2 File Offset: 0x000B3BF2
		// (set) Token: 0x06002403 RID: 9219 RVA: 0x000B59FA File Offset: 0x000B3BFA
		public ItemDropAction Action { get; private set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x000B5A03 File Offset: 0x000B3C03
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x000B5A0B File Offset: 0x000B3C0B
		public bool IsExternal { get; private set; }

		// Token: 0x06002406 RID: 9222 RVA: 0x000B5A14 File Offset: 0x000B3C14
		public ItemDropArgs(object[] dragItems, object dropTarget, ItemDropAction action, bool isExternal)
		{
			this.DragItems = dragItems;
			this.DropTarget = dropTarget;
			this.Action = action;
			this.IsExternal = isExternal;
		}
	}
}
