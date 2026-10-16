using System;
using System.Collections.Generic;
using XRL.Names;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A0A RID: 2570
	public class BaboonHero1 : IObjectBuilder
	{
		// Token: 0x06006D0D RID: 27917 RVA: 0x00247C6C File Offset: 0x00245E6C
		public override void Apply(GameObject GO, string Context)
		{
			int num = Stat.Roll("1d10+2", null);
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"*Rings*",
				num.ToString() + "-ringed"
			} };
			string text = NameMaker.MakeTitle(GO, null, null, null, null, null, null, null, null, null, "Hero", dictionary, true, null, null);
			string text2 = null;
			bool flag = false;
			string text3 = "Hero";
			bool flag2 = true;
			Dictionary<string, string> dictionary2 = dictionary;
			GO.GiveProperName(text2, flag, text3, flag2, null, null, dictionary2, null);
			GO.RequirePart<DisplayNameColor>(false).SetColorByPriority("M", 30);
			if (!text.IsNullOrEmpty())
			{
				GO.RequirePart<Titles>(false).AddTitle(text, -40);
				if (text.Contains("King") || text.Contains("Queen"))
				{
					num += Stat.Random(1, 4);
					GO.RequirePart<Mutations>(false).AddMutation(new MultipleArms(), 2, true);
					GO.ReceiveObjectFromPopulation("Melee Weapons 2", null, false, 50, 0, null, null, null, null, null);
					GO.ReceiveObjectFromPopulation("Melee Weapons 2", null, false, 0, 0, null, null, null, null, null);
					GO.ReceiveObjectFromPopulation("Melee Weapons 1", null, false, 50, 0, null, null, null, null, null);
					GO.ReceiveObjectFromPopulation("Melee Weapons 1", null, false, 0, 0, null, null, null, null, null);
				}
				if (text.Contains("Philanderer") && GO.HasPart<BaboonHero1Pack>())
				{
					GO.GetPart<BaboonHero1Pack>().FollowerMultiplier = 2.5f;
				}
				if (text.Contains("Riddler"))
				{
					GO.RequirePart<Mutations>(false).AddMutation(new Confusion(), 2, true);
				}
				if (text.Contains("Hermit"))
				{
					GO.MultiplyStat("Hitpoints", 2);
					GO.RemovePart<BaboonHero1Pack>();
				}
				if (text.Contains("Sophisticate"))
				{
					GO.BoostStat("Intelligence", Stat.Random(3, 6));
					if (GO.HasPart<BaboonHero1Pack>())
					{
						GO.GetPart<BaboonHero1Pack>().Hat = true;
					}
				}
			}
			GO.ReceiveObjectFromPopulation("Junk 2R", null, false, 0, 0, null, null, null, null, null);
			GO.ReceiveObjectFromPopulation("Junk 3", null, false, 0, 0, null, null, null, null, null);
			GO.MultiplyStat("Hitpoints", 2);
			GO.AddBaseStat("Strength", Stat.Roll(1, 4) + num);
			GO.AddBaseStat("Agility", Stat.Roll(1, 4) + num);
			GO.AddBaseStat("Intelligence", Stat.Roll(1, 4));
			GO.AddBaseStat("Ego", Stat.Roll(1, 4));
			GO.AddBaseStat("Willpower", Stat.Roll(1, 4));
			GO.AddBaseStat("Toughness", Stat.Roll(1, 4));
			GO.AddBaseStat("XPValue", num * 75);
			for (int i = 0; i < num; i++)
			{
				GO.AddBaseStat("Hitpoints", Stat.Random(1, 10));
			}
		}
	}
}
