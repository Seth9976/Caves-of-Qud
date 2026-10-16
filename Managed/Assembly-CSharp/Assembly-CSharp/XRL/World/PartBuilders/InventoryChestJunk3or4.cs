using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D0 RID: 2512
	public class InventoryChestJunk3or4 : IPartBuilder
	{
		// Token: 0x06006B9A RID: 27546 RVA: 0x0022BD31 File Offset: 0x00229F31
		public void BuildPart(IPart iPart, string Context = null)
		{
			if (50.in100())
			{
				ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 3, Context);
				return;
			}
			ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 4, Context);
		}
	}
}
