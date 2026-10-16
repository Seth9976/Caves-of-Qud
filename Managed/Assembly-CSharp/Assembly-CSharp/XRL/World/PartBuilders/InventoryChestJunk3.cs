using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009CF RID: 2511
	public class InventoryChestJunk3 : IPartBuilder
	{
		// Token: 0x06006B98 RID: 27544 RVA: 0x0022BD1A File Offset: 0x00229F1A
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 3, Context);
		}
	}
}
