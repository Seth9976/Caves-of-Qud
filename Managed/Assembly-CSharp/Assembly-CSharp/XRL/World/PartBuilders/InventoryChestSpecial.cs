using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009CB RID: 2507
	public class InventoryChestSpecial
	{
		// Token: 0x06006B90 RID: 27536 RVA: 0x0022BCB4 File Offset: 0x00229EB4
		public static void BuildPart(IPart iPart, int Level, string Context = null)
		{
			ChestBuilders.BuildSpecialChestInventory(iPart as Inventory, Level, Context);
		}
	}
}
