using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A00 RID: 2560
	public class Tier2HumanoidEquipment : IObjectBuilder
	{
		// Token: 0x06006CF8 RID: 27896 RVA: 0x002473A0 File Offset: 0x002455A0
		public override void Apply(GameObject GO, string Context)
		{
			GO.ReceiveObjectFromPopulation("Melee Weapons 2", null, false, 0, 0, null, null, null, Context, null);
			if (75.in100())
			{
				GO.ReceiveObjectFromPopulation("Armor 2", null, false, 0, 0, null, null, null, Context, null);
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 2", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
