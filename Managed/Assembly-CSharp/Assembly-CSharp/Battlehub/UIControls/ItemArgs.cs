using System;

namespace Battlehub.UIControls
{
	// Token: 0x020003C9 RID: 969
	public class ItemArgs : EventArgs
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000B5992 File Offset: 0x000B3B92
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x000B599A File Offset: 0x000B3B9A
		public object[] Items { get; private set; }

		// Token: 0x060023FA RID: 9210 RVA: 0x000B59A3 File Offset: 0x000B3BA3
		public ItemArgs(object[] item)
		{
			this.Items = item;
		}
	}
}
