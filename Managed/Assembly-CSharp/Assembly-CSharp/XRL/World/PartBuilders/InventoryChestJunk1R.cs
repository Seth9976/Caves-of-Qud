using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D6 RID: 2518
	public class InventoryChestJunk1R : IPartBuilder
	{
		// Token: 0x06006BA6 RID: 27558 RVA: 0x0022BDD2 File Offset: 0x00229FD2
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildRareChestInventory(iPart as Inventory, 1, Context);
		}
	}
}
