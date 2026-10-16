using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D3 RID: 2515
	public class InventoryChestJunk6 : IPartBuilder
	{
		// Token: 0x06006BA0 RID: 27552 RVA: 0x0022BD8D File Offset: 0x00229F8D
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 6, Context);
		}
	}
}
