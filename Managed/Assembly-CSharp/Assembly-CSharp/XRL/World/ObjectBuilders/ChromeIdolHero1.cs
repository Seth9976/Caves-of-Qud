using System;
using XRL.Names;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A0B RID: 2571
	public class ChromeIdolHero1 : IObjectBuilder
	{
		// Token: 0x06006D0F RID: 27919 RVA: 0x00247F2C File Offset: 0x0024612C
		public override void Apply(GameObject GO, string Context)
		{
			string text = NameMaker.MakeTitle(GO, null, null, null, null, null, null, null, null, null, "Hero", null, true, null, null);
			GO.GiveProperName(null, false, "Hero", true, null, null, null, null);
			GO.RequirePart<DisplayNameColor>(false).SetColorByPriority("M", 30);
			string text2 = null;
			if (!text.IsNullOrEmpty())
			{
				int num = text.IndexOf(" of ");
				if (num != -1)
				{
					text2 = text.Substring(num + 4);
				}
				GO.RequirePart<Titles>(false).AddTitle(text, -40);
				if (text.Contains("revered") || text.Contains("venerated") || text.Contains("terrible"))
				{
					GO.BoostStat("Ego", Stat.Random(1, 3));
				}
				if (text.Contains("ancient") || text.Contains("corroded") || text.Contains("rusted"))
				{
					GO.BoostStat("Toughness", Stat.Random(1, 3));
				}
				if (text.Contains("joyous"))
				{
					GO.BoostStat("Willpower", Stat.Random(1, 3));
				}
			}
			if (!text2.IsNullOrEmpty())
			{
				string text3 = "NaphtaaliDeityMaxWorshipPower" + text2;
				int num2;
				if (The.Game.HasIntGameState(text3))
				{
					num2 = The.Game.GetIntGameState(text3, 0);
				}
				else
				{
					num2 = Math.Max(Stat.Random(1, 20), Stat.Random(1, 5));
					The.Game.SetIntGameState(text3, num2);
				}
				GO.RequirePart<Shrine>(false);
				GO.SetStringProperty("Worshippable", "yes", false);
				GO.SetStringProperty("WorshippedAs", text2, false);
				GO.SetIntProperty("WorshipPower", Stat.Random(1, num2), false);
			}
			GO.ReceiveObjectFromPopulation("Melee Weapons 2", null, false, 50, 0, null, null, null, Context, null);
			GO.ReceiveObjectFromPopulation("Armor 2", null, false, 50, 0, null, null, null, Context, null);
			GO.ReceiveObjectFromPopulation("Junk 3R", null, false, 0, 0, null, null, null, Context, null);
			GO.ReceiveObjectFromPopulation("Junk 3", null, false, 0, 0, null, null, null, Context, null);
			GO.MultiplyStat("Hitpoints", 2);
		}
	}
}
