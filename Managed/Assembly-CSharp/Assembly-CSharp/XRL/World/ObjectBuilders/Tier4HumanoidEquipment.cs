using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A06 RID: 2566
	public class Tier4HumanoidEquipment : IObjectBuilder
	{
		// Token: 0x06006D04 RID: 27908 RVA: 0x00247904 File Offset: 0x00245B04
		public override void Apply(GameObject GO, string Context)
		{
			GO.ReceiveObjectFromPopulation("Melee Weapons 4", null, false, 0, 0, null, null, null, Context, null);
			if (75.in100())
			{
				GO.ReceiveObjectFromPopulation("Armor 4", null, false, 0, 0, null, null, null, Context, null);
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 4", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
