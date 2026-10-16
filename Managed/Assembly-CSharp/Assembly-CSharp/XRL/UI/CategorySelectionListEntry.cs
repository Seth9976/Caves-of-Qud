using System;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014A1 RID: 5281
	public class CategorySelectionListEntry
	{
		// Token: 0x0600CF6C RID: 53100 RVA: 0x004BC6A4 File Offset: 0x004BA8A4
		public CategorySelectionListEntry(InventoryCategory Cat)
		{
			this.Category = Cat;
		}

		// Token: 0x0600CF6D RID: 53101 RVA: 0x004BC6B3 File Offset: 0x004BA8B3
		public CategorySelectionListEntry(GameObject GO)
		{
			this.Object = GO;
		}

		// Token: 0x04005095 RID: 20629
		public InventoryCategory Category;

		// Token: 0x04005096 RID: 20630
		public GameObject Object;
	}
}
