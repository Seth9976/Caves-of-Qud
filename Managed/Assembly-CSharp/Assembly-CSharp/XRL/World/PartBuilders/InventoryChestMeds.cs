using System;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009CE RID: 2510
	public class InventoryChestMeds : IPartBuilder
	{
		// Token: 0x06006B96 RID: 27542 RVA: 0x0022BCF9 File Offset: 0x00229EF9
		public void BuildPart(IPart iPart, string Context = null)
		{
			ChestBuilders.BuildTableChestInventory(iPart as Inventory, "Meds ", "1d6+1", 3, Context);
		}
	}
}
