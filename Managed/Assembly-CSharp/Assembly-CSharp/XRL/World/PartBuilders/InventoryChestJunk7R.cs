using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009DC RID: 2524
	public class InventoryChestJunk7R : IPartBuilder
	{
		// Token: 0x06006BB2 RID: 27570 RVA: 0x0022BE5C File Offset: 0x0022A05C
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildRareChestInventory(iPart as Inventory, 7, Context);
		}
	}
}
