using System;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A13 RID: 2579
	public static class SnapjawHero
	{
		// Token: 0x06006D2B RID: 27947 RVA: 0x00248C78 File Offset: 0x00246E78
		public static void ApplySnapjawTraits(GameObject GO, string title, string Context = null)
		{
			if (title.IsNullOrEmpty())
			{
				return;
			}
			if (title.Contains("fleet-footed"))
			{
				GO.AddBaseStat("MoveSpeed", -Stat.Random(25, 50));
			}
			if (title.Contains("learned"))
			{
				GO.BoostStat("Intelligence", Stat.Random(1, 3));
			}
			if (title.Contains("stalwart"))
			{
				GO.AddBaseStat("Hitpoints", Stat.Random(10, 20));
			}
			if (title.Contains("fearsome"))
			{
				GO.BoostStat("Ego", Stat.Random(1, 3));
			}
			if (title.Contains("nimble"))
			{
				GO.AddBaseStat("DV", Stat.Random(3, 6));
			}
			if (title.Contains("hulking"))
			{
				GO.BoostStat("Strength", Stat.Random(1, 3));
			}
			if (title.Contains("calloused") && !GO.HasSkill("Endurance_Calloused"))
			{
				GO.AddSkill("Endurance_Calloused", null, null);
			}
			if (title.Contains("Skullsplitter"))
			{
				GO.ReceiveObject(GameObject.Create("Steel Battle Axe", 0, 0, null, null, null, Context, null), false, null);
				Skills part = GO.GetPart<Skills>();
				part.AddSkill(new Axe(), null, null);
				part.AddSkill(new Axe_Dismember(), null, null);
			}
			if (title.Contains("Firesnarler"))
			{
				GO.GetPart<Mutations>().AddMutation(new Pyrokinesis(), 2, true);
			}
			if (title.Contains("Bear-baiter"))
			{
				GO.AddBaseStat("DV", 1);
			}
			if (title.Contains("Tot-eater"))
			{
				GO.BoostStat("Agility", Stat.Random(1, 3));
			}
			if (title.Contains("Gutspiller"))
			{
				GO.RequirePart<Mutations>(false).AddMutation(new Horns(), 2, true);
			}
			if (title.Contains("King"))
			{
				GO.BoostStat("Strength", Stat.Random(1, 3));
				GO.BoostStat("Ego", Stat.Random(1, 3));
				GO.BoostStat("Willpower", Stat.Random(1, 3));
				GO.AddBaseStat("Hitpoints", Stat.Random(10, 20));
			}
		}
	}
}
