using System;
using XRL.World.Parts;

namespace XRL.World.Biomes
{
	// Token: 0x02000A2D RID: 2605
	public static class PutridTemplate
	{
		// Token: 0x06006D84 RID: 28036 RVA: 0x0024C568 File Offset: 0x0024A768
		public static void Apply(GameObject GO)
		{
			GO.SetIntProperty("Putrid", 1, false);
			if (GO.Render != null && GO != null && GO.HasPart<Body>())
			{
				GO.Render.DisplayName = "putrid " + GO.Render.DisplayName;
				GO.Render.ColorString = "&g^W";
				GO.AddPart<VomitOnHit>(new VomitOnHit(), true, false);
			}
			if (GO.HasStat("Hitpoints"))
			{
				GO.Statistics["Hitpoints"].BaseValue *= 2;
			}
			if (GO.HasStat("AV"))
			{
				GO.Statistics["AV"].BaseValue += 3;
			}
			GO.AddPart<SpawnOnDeath>(new SpawnOnDeath("Bloatfly"), true, false);
		}
	}
}
