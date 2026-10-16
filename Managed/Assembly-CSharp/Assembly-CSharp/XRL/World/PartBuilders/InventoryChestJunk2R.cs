using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D7 RID: 2519
	public class InventoryChestJunk2R : IPartBuilder
	{
		// Token: 0x06006BA8 RID: 27560 RVA: 0x0022BDE9 File Offset: 0x00229FE9
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildRareChestInventory(iPart as Inventory, 2, Context);
		}
	}
}
