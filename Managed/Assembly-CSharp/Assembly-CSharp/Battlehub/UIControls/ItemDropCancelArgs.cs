using System;

namespace Battlehub.UIControls
{
	// Token: 0x020003CA RID: 970
	public class ItemDropCancelArgs : ItemDropArgs
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000B59B2 File Offset: 0x000B3BB2
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x000B59BA File Offset: 0x000B3BBA
		public bool Cancel { get; set; }

		// Token: 0x060023FD RID: 9213 RVA: 0x000B59C3 File Offset: 0x000B3BC3
		public ItemDropCancelArgs(object[] dragItems, object dropTarget, ItemDropAction action, bool isExternal)
			: base(dragItems, dropTarget, action, isExternal)
		{
		}
	}
}
