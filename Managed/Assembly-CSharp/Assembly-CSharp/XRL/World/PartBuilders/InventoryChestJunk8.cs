using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009D5 RID: 2517
	public class InventoryChestJunk8 : IPartBuilder
	{
		// Token: 0x06006BA4 RID: 27556 RVA: 0x0022BDBB File Offset: 0x00229FBB
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildCommonChestInventory(iPart as Inventory, 8, Context);
		}
	}
}
