using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009CA RID: 2506
	public class InventoryChestJunk : IPartBuilder
	{
		// Token: 0x06006B8E RID: 27534 RVA: 0x0022BC38 File Offset: 0x00229E38
		public void BuildPart(IPart iPart, string Context = null)
		{
			Inventory inventory = iPart as Inventory;
			bool flag = false;
			int i = 0;
			int num = Stat.Random(1, 10);
			while (i < num)
			{
				try
				{
					inventory.AddObject(PopulationManager.CreateOneFrom("Junk 1", null, 0, 0, null, Context, null, null, null), null, false, false, true, null);
				}
				catch (Exception ex)
				{
					if (!flag)
					{
						MetricsManager.LogError("exception in InventoryChestJunk.BuildPart()", ex);
						flag = true;
					}
				}
				i++;
			}
		}
	}
}
