using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D8 RID: 2520
	public class InventoryChestJunk3R : IPartBuilder
	{
		// Token: 0x06006BAA RID: 27562 RVA: 0x0022BE00 File Offset: 0x0022A000
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildRareChestInventory(iPart as Inventory, 3, Context);
		}
	}
}
