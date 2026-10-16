using System;
using XRL.Rules;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x020009FF RID: 2559
	public class Tier1HumanoidMissile : IObjectBuilder
	{
		// Token: 0x06006CF6 RID: 27894 RVA: 0x0024735C File Offset: 0x0024555C
		public override void Apply(GameObject GO, string Context)
		{
			GO.ReceiveObject("Short Bow", false, 0, 0, null, null, null, Context);
			GO.ReceiveObject("Wooden Arrow", Stat.Random(20, 40), false, 0, 0, null, null, null, Context);
		}
	}
}
