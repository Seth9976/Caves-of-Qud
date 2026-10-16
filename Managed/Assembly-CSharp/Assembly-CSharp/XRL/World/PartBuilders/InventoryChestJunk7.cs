using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D4 RID: 2516
	public class InventoryChestJunk7 : IPartBuilder
	{
		// Token: 0x06006BA2 RID: 27554 RVA: 0x0022BDA4 File Offset: 0x00229FA4
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 7, Context);
		}
	}
}
