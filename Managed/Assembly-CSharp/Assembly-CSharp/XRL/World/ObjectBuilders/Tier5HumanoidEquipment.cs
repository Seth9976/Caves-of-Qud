using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A07 RID: 2567
	public class Tier5HumanoidEquipment : IObjectBuilder
	{
		// Token: 0x06006D06 RID: 27910 RVA: 0x0024796C File Offset: 0x00245B6C
		public override void Apply(GameObject GO, string Context)
		{
			GO.ReceiveObjectFromPopulation("Melee Weapons 5", null, false, 0, 0, null, null, null, Context, null);
			if (75.in100())
			{
				GO.ReceiveObjectFromPopulation("Armor 5", null, false, 0, 0, null, null, null, Context, null);
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 5", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
