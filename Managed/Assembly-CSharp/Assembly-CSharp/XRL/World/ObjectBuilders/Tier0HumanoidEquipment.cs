using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x020009FD RID: 2557
	public class Tier0HumanoidEquipment : IObjectBuilder
	{
		// Token: 0x06006CF2 RID: 27890 RVA: 0x0024728C File Offset: 0x0024548C
		public override void Apply(GameObject GO, string Context)
		{
			GO.ReceiveObjectFromPopulation("Melee Weapons 1", null, false, 0, 0, null, null, null, Context, null);
			if (75.in100())
			{
				GO.ReceiveObjectFromPopulation("Armor 1", null, false, 0, 0, null, null, null, Context, null);
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 0", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
