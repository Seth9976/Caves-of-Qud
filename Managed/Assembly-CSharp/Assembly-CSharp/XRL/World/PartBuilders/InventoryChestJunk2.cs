using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009CD RID: 2509
	public class InventoryChestJunk2 : IPartBuilder
	{
		// Token: 0x06006B94 RID: 27540 RVA: 0x0022BCE2 File Offset: 0x00229EE2
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 2, Context);
		}
	}
}
