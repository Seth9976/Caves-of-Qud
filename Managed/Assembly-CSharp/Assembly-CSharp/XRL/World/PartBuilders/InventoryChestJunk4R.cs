using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D9 RID: 2521
	public class InventoryChestJunk4R : IPartBuilder
	{
		// Token: 0x06006BAC RID: 27564 RVA: 0x0022BE17 File Offset: 0x0022A017
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildRareChestInventory(iPart as Inventory, 4, Context);
		}
	}
}
