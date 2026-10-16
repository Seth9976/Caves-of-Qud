using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A03 RID: 2563
	public class Tier3HumanoidEquipment : IObjectBuilder
	{
		// Token: 0x06006CFE RID: 27902 RVA: 0x002474EC File Offset: 0x002456EC
		public override void Apply(GameObject GO, string Context)
		{
			GO.ReceiveObjectFromPopulation("Melee Weapons 3", null, false, 0, 0, null, null, null, Context, null);
			if (75.in100())
			{
				GO.ReceiveObjectFromPopulation("Armor 3", null, false, 0, 0, null, null, null, Context, null);
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 3", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
