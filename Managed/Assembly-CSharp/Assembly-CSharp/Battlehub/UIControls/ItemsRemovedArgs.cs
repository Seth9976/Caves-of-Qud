using System;

namespace Battlehub.UIControls
{
	// Token: 0x020003C7 RID: 967
	public class ItemsRemovedArgs : EventArgs
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x000B58EF File Offset: 0x000B3AEF
		// (set) Token: 0x060023E9 RID: 9193 RVA: 0x000B58F7 File Offset: 0x000B3AF7
		public object[] Items { get; private set; }

		// Token: 0x060023EA RID: 9194 RVA: 0x000B5900 File Offset: 0x000B3B00
		public ItemsRemovedArgs(object[] items)
		{
			this.Items = items;
		}
	}
}
