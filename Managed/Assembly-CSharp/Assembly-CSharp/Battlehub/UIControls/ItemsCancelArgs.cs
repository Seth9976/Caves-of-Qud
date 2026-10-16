using System;
using System.Collections.Generic;

namespace Battlehub.UIControls
{
	// Token: 0x020003C6 RID: 966
	public class ItemsCancelArgs : EventArgs
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x000B58CF File Offset: 0x000B3ACF
		// (set) Token: 0x060023E6 RID: 9190 RVA: 0x000B58D7 File Offset: 0x000B3AD7
		public List<object> Items { get; set; }

		// Token: 0x060023E7 RID: 9191 RVA: 0x000B58E0 File Offset: 0x000B3AE0
		public ItemsCancelArgs(List<object> items)
		{
			this.Items = items;
		}
	}
}
