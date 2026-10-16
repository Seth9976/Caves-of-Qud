using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009DA RID: 2522
	public class InventoryChestJunk5R : IPartBuilder
	{
		// Token: 0x06006BAE RID: 27566 RVA: 0x0022BE2E File Offset: 0x0022A02E
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildRareChestInventory(iPart as Inventory, 5, Context);
		}
	}
}
