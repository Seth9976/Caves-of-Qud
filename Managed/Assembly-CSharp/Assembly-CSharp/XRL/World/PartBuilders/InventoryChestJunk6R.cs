using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009DB RID: 2523
	public class InventoryChestJunk6R : IPartBuilder
	{
		// Token: 0x06006BB0 RID: 27568 RVA: 0x0022BE45 File Offset: 0x0022A045
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildRareChestInventory(iPart as Inventory, 6, Context);
		}
	}
}
