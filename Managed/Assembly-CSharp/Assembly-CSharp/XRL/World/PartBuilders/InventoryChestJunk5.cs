using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D2 RID: 2514
	public class InventoryChestJunk5 : IPartBuilder
	{
		// Token: 0x06006B9E RID: 27550 RVA: 0x0022BD76 File Offset: 0x00229F76
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 5, Context);
		}
	}
}
