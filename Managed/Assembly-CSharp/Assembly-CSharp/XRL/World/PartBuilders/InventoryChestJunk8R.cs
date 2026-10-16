using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009DD RID: 2525
	public class InventoryChestJunk8R : IPartBuilder
	{
		// Token: 0x06006BB4 RID: 27572 RVA: 0x0022BE73 File Offset: 0x0022A073
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildRareChestInventory(iPart as Inventory, 8, Context);
		}
	}
}
