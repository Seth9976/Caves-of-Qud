using System;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A12 RID: 2578
	public class MechanimistPaladin : IObjectBuilder
	{
		// Token: 0x06006D29 RID: 27945 RVA: 0x00248BD8 File Offset: 0x00246DD8
		public override void Apply(GameObject GO, string Context)
		{
			GameObject gameObject;
			if (75.in100())
			{
				gameObject = GameObject.Create("Cudgel3", 0, 0, null, null, null, Context, null);
			}
			else
			{
				gameObject = GameObject.Create("Cudgel4", 0, 0, null, null, null, Context, null);
			}
			if (50.in100() && !gameObject.HasPart<ModFreezing>())
			{
				gameObject.AddPart<ModFreezing>(new ModFreezing(4), true, false);
			}
			GO.ReceiveObject(gameObject, false, null);
			if (50.in100())
			{
				GO.ReceiveObject("Leather Boots", false, 0, 0, null, null, null, null);
				return;
			}
			GO.ReceiveObject("Steel Boots", false, 0, 0, null, null, null, null);
		}
	}
}
