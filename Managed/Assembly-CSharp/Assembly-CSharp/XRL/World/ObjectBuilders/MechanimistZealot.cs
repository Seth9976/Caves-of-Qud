using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A11 RID: 2577
	public class MechanimistZealot : IObjectBuilder
	{
		// Token: 0x06006D27 RID: 27943 RVA: 0x00248B8C File Offset: 0x00246D8C
		public override void Apply(GameObject GO, string Context)
		{
			if (75.in100())
			{
				GO.ReceiveObjectFromPopulation("Melee Weapons 3", null, false, 0, 0, null, null, null, Context, null);
				return;
			}
			GO.ReceiveObjectFromPopulation("Melee Weapons 4", null, false, 0, 0, null, null, null, Context, null);
		}
	}
}
