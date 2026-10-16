using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009CC RID: 2508
	public class InventoryChestJunk1 : IPartBuilder
	{
		// Token: 0x06006B92 RID: 27538 RVA: 0x0022BCCB File Offset: 0x00229ECB
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 1, Context);
		}
	}
}
