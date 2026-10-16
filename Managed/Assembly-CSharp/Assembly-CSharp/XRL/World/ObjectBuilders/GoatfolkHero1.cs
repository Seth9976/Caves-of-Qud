using System;
using System.Collections.Generic;
using XRL.Names;
using XRL.Rules;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A10 RID: 2576
	public class GoatfolkHero1 : IObjectBuilder
	{
		// Token: 0x06006D24 RID: 27940 RVA: 0x002487CA File Offset: 0x002469CA
		public override void Initialize()
		{
			this.ForceTitle = null;
			this.ForceName = null;
		}

		// Token: 0x06006D25 RID: 27941 RVA: 0x002487DC File Offset: 0x002469DC
		public override void Apply(GameObject GO, string Context)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!this.ForceTitle.IsNullOrEmpty())
			{
				dictionary["*Position*"] = this.ForceTitle;
			}
			string text = NameMaker.MakeEpithet(GO, null, null, null, null, null, null, null, null, null, "Hero", dictionary, true, null, null);
			string text2 = NameMaker.MakeTitle(GO, null, null, null, null, null, null, null, null, null, "Hero", dictionary, true, null, null);
			string forceName = this.ForceName;
			bool flag = !this.ForceName.IsNullOrEmpty();
			string text3 = "Hero";
			bool flag2 = true;
			Dictionary<string, string> dictionary2 = dictionary;
			if (GO.GiveProperName(forceName, flag, text3, flag2, null, null, dictionary2, null) == null)
			{
				string shortDisplayName = GO.ShortDisplayName;
			}
			GO.RequirePart<DisplayNameColor>(false).SetColorByPriority("M", 30);
			if (!text.IsNullOrEmpty())
			{
				GO.RequirePart<Epithets>(false).AddEpithet(text, -40);
			}
			if (!text2.IsNullOrEmpty())
			{
				GO.RequirePart<Titles>(false).AddTitle(text2, -40);
			}
			Mutations mutations = GO.RequirePart<Mutations>(false);
			if (text == "Stargazer")
			{
				GO.BoostStat("Intelligence", 2);
				if (!mutations.HasMutation("LightManipulation"))
				{
					mutations.AddMutation(new LightManipulation(), 5, true);
				}
			}
			else if (text == "Heartbiter")
			{
				if (!mutations.HasMutation("AdrenalControl2"))
				{
					mutations.AddMutation(new AdrenalControl2(), 1, true);
				}
			}
			else if (text == "Twicetalker")
			{
				if (!mutations.HasMutation("TwoHeaded"))
				{
					mutations.AddMutation(new TwoHeaded(), 1, true);
					GO.ReceiveObject(GameObject.Create("Goatfolk_Horns", 0, 0, null, null, null, Context, null), false, null);
				}
			}
			else if (text == "Souldrinker")
			{
				if (!mutations.HasMutation("Syphon Vim"))
				{
					mutations.AddMutation(new LifeDrain(), 1, true);
				}
			}
			else if (text == "Whitefinger")
			{
				if (!mutations.HasMutation("ElectricalGeneration"))
				{
					mutations.AddMutation(new ElectricalGeneration(), 4, true);
				}
			}
			else if (text == "Clovenhorn")
			{
				GO.BoostStat("Strength", 1);
				if (!mutations.HasMutation("Horns"))
				{
					mutations.AddMutation(new Horns(), Stat.Random(5, 6), true);
				}
				if (!GO.HasSkill("Tactics_Charge"))
				{
					GO.AddSkill("Tactics_Charge", null, null);
				}
			}
			if (!text2.IsNullOrEmpty())
			{
				if (text2.Contains("Clan Hotur"))
				{
					GO.BoostStat("Strength", 1);
				}
				if (text2.Contains("Clan Ibex"))
				{
					mutations.AddMutation(new Horns(), 2, true);
				}
				if (text2.Contains("Clan Sol"))
				{
					if (!mutations.HasMutation("PhotosyntheticSkin"))
					{
						mutations.AddMutation(new PhotosyntheticSkin(), 4, true);
					}
					GoatfolkClan1 part = GO.GetPart<GoatfolkClan1>();
					if (part != null)
					{
						part.Photosynthetic = true;
					}
				}
				if (text2.Contains("Clan Whitetongue"))
				{
					GO.BoostStat("Intelligence", 1);
					GO.BoostStat("Ego", 1);
					GO.BoostStat("Willpower", 1);
				}
				if (text2.Contains("Clan Yr"))
				{
					GO.BoostStat("MoveSpeed", -0.75);
				}
				if (text2.Contains("Clan Mnim"))
				{
					GO.BoostStat("Toughness", 1);
				}
			}
			GO.ReceiveObjectFromPopulation("Armor 4", null, false, 50, 0, null, null, null, Context, null);
			GO.ReceiveObjectFromPopulation("Junk 4", null, false, 0, 0, null, null, null, Context, null);
			GO.ReceiveObjectFromPopulation("Junk 4R", null, false, 0, 0, null, null, null, Context, null);
			GO.MultiplyStat("Hitpoints", 2);
		}

		// Token: 0x040031E3 RID: 12771
		public string ForceTitle;

		// Token: 0x040031E4 RID: 12772
		public string ForceName;
	}
}
