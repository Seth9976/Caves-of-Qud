using System;
using System.Collections;

namespace Battlehub.UIControls
{
	// Token: 0x020003D6 RID: 982
	public class ItemExpandingArgs : EventArgs
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x000B841E File Offset: 0x000B661E
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x000B8426 File Offset: 0x000B6626
		public object Item { get; private set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x000B842F File Offset: 0x000B662F
		// (set) Token: 0x06002494 RID: 9364 RVA: 0x000B8437 File Offset: 0x000B6637
		public IEnumerable Children { get; set; }

		// Token: 0x06002495 RID: 9365 RVA: 0x000B8440 File Offset: 0x000B6640
		public ItemExpandingArgs(object item)
		{
			this.Item = item;
		}
	}
}
