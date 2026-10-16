using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x020009FE RID: 2558
	public class Tier1HumanoidEquipment : IObjectBuilder
	{
		// Token: 0x06006CF4 RID: 27892 RVA: 0x002472F4 File Offset: 0x002454F4
		public override void Apply(GameObject GO, string Context)
		{
			GO.ReceiveObjectFromPopulation("Melee Weapons 1", null, false, 0, 0, null, null, null, Context, null);
			if (75.in100())
			{
				GO.ReceiveObjectFromPopulation("Armor 1", null, false, 0, 0, null, null, null, Context, null);
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 1", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
