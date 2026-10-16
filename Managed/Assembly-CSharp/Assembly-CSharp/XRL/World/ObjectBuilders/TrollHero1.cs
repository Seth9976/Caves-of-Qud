using System;
using XRL.Names;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A16 RID: 2582
	public class TrollHero1 : IObjectBuilder
	{
		// Token: 0x06006D30 RID: 27952 RVA: 0x002490A8 File Offset: 0x002472A8
		public override void Apply(GameObject GO, string Context)
		{
			string text = NameMaker.MakeTitle(GO, null, null, null, null, null, null, null, null, null, "Hero", null, true, null, null);
			GO.GiveProperName(null, false, "Hero", true, null, null, null, null);
			GO.RequirePart<DisplayNameColor>(false).SetColorByPriority("M", 30);
			if (!text.IsNullOrEmpty())
			{
				GO.RequirePart<Titles>(false).AddTitle(text, -40);
				if (text.Contains("frenetic"))
				{
					GO.AddBaseStat("Speed", Stat.Random(25, 50));
				}
				if (text.Contains("learned"))
				{
					GO.BoostStat("Intelligence", Stat.Random(1, 3));
				}
				if (text.Contains("everlasting"))
				{
					GO.AddBaseStat("Hitpoints", Stat.Random(40, 60));
				}
				if (text.Contains("bestial"))
				{
					GO.BoostStat("Ego", Stat.Random(1, 3));
				}
				if (text.Contains("bestial"))
				{
					GO.BoostStat("Ego", Stat.Random(1, 3));
				}
				if (text.Contains("bloodthirsty"))
				{
					GO.BoostStat("Agility", Stat.Random(1, 3));
				}
				if (text.Contains("hulking"))
				{
					GO.BoostStat("Strength", Stat.Random(1, 3));
				}
				if (text.Contains("rubberhide"))
				{
					GO.AddBaseStat("AV", 4);
				}
				if (text.Contains("Skull-collector"))
				{
					GO.ReceiveObject(GameObject.Create("Battle Axe4", 0, 0, null, null, null, Context, null), false, null);
					if (!GO.HasSkill("Axe_Dismember"))
					{
						GO.AddSkill("Axe_Dismember", null, null);
					}
				}
				if (text.Contains("Heart-eater"))
				{
					GO.BoostStat("Strength", Stat.Random(1, 3));
				}
				if (text.Contains("Hunt-master"))
				{
					GO.AddBaseStat("DV", 4);
				}
				if (text.Contains("Man-eater"))
				{
					GO.BoostStat("Agility", Stat.Random(1, 3));
				}
				if (text.Contains("two-headed"))
				{
					GO.RequirePart<Mutations>(false).AddMutation(new TwoHeaded(), 2, true);
				}
				if (text.Contains("Caveking"))
				{
					GO.BoostStat("Strength", Stat.Random(1, 3));
					GO.BoostStat("Ego", Stat.Random(1, 3));
					GO.BoostStat("Willpower", Stat.Random(1, 3));
					GO.AddBaseStat("Hitponts", Stat.Random(10, 20));
				}
			}
			GO.ReceiveObjectFromPopulation("Armor 2", null, false, 50, 0, null, null, null, Context, null);
			GO.ReceiveObjectFromPopulation("Junk 4R", null, false, 0, 0, null, null, null, Context, null);
			GO.ReceiveObjectFromPopulation("Junk 4", null, false, 0, 0, null, null, null, Context, null);
		}
	}
}
