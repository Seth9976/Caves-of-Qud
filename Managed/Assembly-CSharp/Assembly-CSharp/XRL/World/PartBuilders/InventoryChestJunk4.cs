using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D1 RID: 2513
	public class InventoryChestJunk4 : IPartBuilder
	{
		// Token: 0x06006B9C RID: 27548 RVA: 0x0022BD5F File Offset: 0x00229F5F
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 4, Context);
		}
	}
}
