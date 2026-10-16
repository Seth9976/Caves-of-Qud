using System;
using XRL.Names;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A15 RID: 2581
	public class SnapjawHero0 : IObjectBuilder
	{
		// Token: 0x06006D2E RID: 27950 RVA: 0x00248F9C File Offset: 0x0024719C
		public override void Apply(GameObject GO, string Context)
		{
			string text = NameMaker.MakeTitle(GO, null, null, null, null, null, null, null, null, null, "Hero", null, true, null, null);
			GO.GiveProperName(null, false, "Hero", true, null, null, null, null);
			if (!text.IsNullOrEmpty())
			{
				GO.RequirePart<Titles>(false).AddTitle(text, -40);
			}
			GO.RequirePart<DisplayNameColor>(false).SetColorByPriority("M", 30);
			SnapjawHero.ApplySnapjawTraits(GO, text, Context);
			GO.ReceiveObjectFromPopulation("Melee Weapons 1", null, false, 25, 0, null, null, null, Context, null);
			GO.ReceiveObjectFromPopulation("Armor 1", null, false, 25, 0, null, null, null, Context, null);
			GO.GetStat("Hitpoints").BaseValue *= 2;
			GO.ReceiveObjectFromPopulation("Junk 1R", null, false, 0, 0, null, null, null, Context, null);
			if (50.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 2", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
