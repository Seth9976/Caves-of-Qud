using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.World;
using XRL.World.Skills;

namespace XRL.Annals
{
	// Token: 0x02000621 RID: 1569
	[Serializable]
	public class BecomesKnownFor : HistoricEvent
	{
		// Token: 0x060041D4 RID: 16852 RVA: 0x001510D8 File Offset: 0x0014F2D8
		public BecomesKnownFor()
		{
			this.bVillageZero = false;
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x001510E7 File Offset: 0x0014F2E7
		public BecomesKnownFor(bool VillageZero)
		{
			this.bVillageZero = VillageZero;
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x001510F8 File Offset: 0x0014F2F8
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapVillage = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			for (;;)
			{
				int num = base.Random(0, 40);
				if (num < 20)
				{
					if (!snapVillage.hasProperty("signatureItem") && !snapVillage.hasProperty("signatureHistoricObjectType"))
					{
						break;
					}
				}
				else
				{
					if (num < 40)
					{
						goto Block_4;
					}
					if (num < 60)
					{
						goto Block_17;
					}
					if (!snapVillage.hasProperty("signatureItem") && !snapVillage.hasProperty("signatureHistoricObjectType"))
					{
						goto Block_19;
					}
				}
			}
			GameObject anItem = EncountersAPI.GetAnItem((GameObjectBlueprint ob) => ob.Tier <= snapVillage.Tier && ob.HasPart("Physics") && ob.GetPartParameter<bool>("Physics", "Takeable", true) && ob.GetPartParameter<int>("Physics", "Weight", 0) <= 100 && !ob.HasTag("Corpse", false) && !ob.HasTag("ExcludeFromQuests", false));
			string blueprint = anItem.Blueprint;
			base.SetEntityProperty("signatureItem", blueprint);
			base.AddEntityListItem("sacredThings", anItem.GetReferenceDisplayName(int.MaxValue, null, "BecomesKnownForSacred", false, true, false, true, true, false, true, false, null, false), false);
			base.AddEntityListItem("profaneThings", base.ExpandString("those who <spice.commonPhrases.profane.!random> ") + anItem.GetPluralName(false, false, true, true), false);
			string text = MutantNameMaker.MakeMutantName();
			string text2 = Grammar.InitCap(string.Format("{0}, on the {1}, {2} founded {3} with {4} in each hand. {5}, the villagers of {3} were known as the people of {7}.|{8}", new object[]
			{
				base.ExpandString("<spice.commonPhrases.yearsAgo.!random>"),
				base.ExpandString("<spice.myth.mythicDays.!random>"),
				text,
				snapVillage.GetProperty("name", "unknown"),
				anItem.GetReferenceDisplayName(int.MaxValue, null, "BecomesKnownForAppear", false, true, false, true, true, false, true, false, null, false),
				Grammar.InitCap(base.ExpandString("<spice.commonPhrases.fromThenOn.!random>").Replace("<entity.name>", Grammar.MakePossessive(text))),
				base.ExpandString("<spice.commonPhrases.cult.!random>"),
				anItem.GetReferenceDisplayName(int.MaxValue, null, "BecomesKnownForPeople", false, true, false, true, true, false, false, true, null, false),
				this.id
			}));
			base.AddEntityListItem("Gospels", text2, false);
			return;
			Block_4:
			List<PowerEntry> list = new List<PowerEntry>(200);
			foreach (SkillEntry skillEntry in SkillFactory.Factory.SkillList.Values)
			{
				foreach (PowerEntry powerEntry in skillEntry.Powers.Values)
				{
					if (!string.IsNullOrEmpty(powerEntry.Class) && powerEntry.Cost > 0)
					{
						list.Add(powerEntry);
					}
				}
			}
			PowerEntry powerEntry2 = null;
			SkillEntry skillEntry2 = null;
			string text3 = "";
			if (this.bVillageZero)
			{
				string property = snapVillage.GetProperty("region", "unknown");
				if (property == "Saltmarsh")
				{
					powerEntry2 = list.Find((PowerEntry p) => p.Class == "CookingAndGathering_Harvestry");
				}
				else if (property == "Saltdunes")
				{
					powerEntry2 = list.Find((PowerEntry p) => p.Class == "Discipline_FastingWay");
				}
				else if (property == "DesertCanyon")
				{
					skillEntry2 = SkillFactory.Factory.SkillByClass["Survival"];
				}
				else
				{
					powerEntry2 = list.Find((PowerEntry p) => p.Class == "CookingAndGathering_Butchery");
				}
			}
			else if (Stat.Random(1, list.Count + SkillFactory.Factory.SkillList.Count) <= list.Count)
			{
				powerEntry2 = list.GetRandomElement<PowerEntry>(null);
				if (powerEntry2.IsSkillInitiatory)
				{
					skillEntry2 = powerEntry2.ParentSkill;
					powerEntry2 = null;
				}
			}
			else
			{
				skillEntry2 = SkillFactory.Factory.SkillList.GetRandomElement<KeyValuePair<string, SkillEntry>>(null).Value;
			}
			if (powerEntry2 != null)
			{
				base.SetEntityProperty("signatureSkill", powerEntry2.Class);
				base.AddEntityListItem("sacredThings", "the " + base.ExpandString("<spice.commonPhrases.practice.!random>") + " of " + powerEntry2.Snippet, false);
				base.AddEntityListItem("profaneThings", "the " + base.ExpandString("<spice.commonPhrases.misuse.!random>") + " of " + powerEntry2.Snippet, false);
				base.AddEntityListItem("itemAdjectiveRoots", powerEntry2.Name, false);
				text3 = powerEntry2.Snippet;
			}
			else if (skillEntry2 != null)
			{
				base.SetEntityProperty("signatureSkill", skillEntry2.Class);
				base.AddEntityListItem("sacredThings", "the " + base.ExpandString("<spice.commonPhrases.practice.!random>") + " of " + skillEntry2.Snippet, false);
				base.AddEntityListItem("profaneThings", "the " + base.ExpandString("<spice.commonPhrases.misuse.!random>") + " of " + skillEntry2.Snippet, false);
				base.AddEntityListItem("itemAdjectiveRoots", skillEntry2.Name, false);
				text3 = skillEntry2.Snippet;
			}
			string text4 = Grammar.RandomShePronoun();
			string text5 = Grammar.InitCap(string.Format("{0}, {1} from {2} {3} through land {4}. There {5} {6} {7} and {8} {9} who taught {10} the way of {11}. {14} returned home and taught what {5} had learned to the {12} of {13}.|{15}", new object[]
			{
				base.ExpandString("<spice.instancesOf.openingTime.!random>").Replace("*year*", "%" + base.Random(1, (int)this.year).ToString() + "%"),
				Grammar.A(base.ExpandString("<spice.personNouns.!random>"), false),
				snapVillage.GetProperty("name", "unknown"),
				base.ExpandString("<spice.commonPhrases.trekked.!random>"),
				base.ExpandString("<spice.history.regions.terrain." + snapVillage.GetProperty("region", "unknown") + ".over.!random>"),
				text4,
				"visited",
				Grammar.A(base.ExpandString("<spice.professions.!random.guildhall>"), false),
				"met",
				Grammar.A(base.ExpandString("<spice.personNouns.!random>"), false),
				Grammar.ObjectPronoun(text4),
				text3,
				base.ExpandString("<spice.commonPhrases.cult.!random>"),
				snapVillage.GetProperty("name", "unknown"),
				Grammar.InitCap(text4),
				this.id
			}));
			base.AddEntityListItem("Gospels", text5, false);
			return;
			Block_17:
			string randomElement = BecomesKnownFor.Temperaments.GetRandomElement<string>(null);
			base.SetEntityProperty("signatureTemperament", randomElement);
			string randomProperty = QudHistoryHelpers.GetRandomProperty(snapVillage, snapVillage.GetProperty("defaultSacredThing", "unknown"), new string[] { "sacredThings" });
			string text6 = Grammar.InitCap(string.Format("{0}, {1} from a nearby {2} visited {3} and insulted {4}. The villagers of {3} responded with great {5} and had {6} {7}.|{8}", new object[]
			{
				base.ExpandString("<spice.instancesOf.openingTime.!random>").Replace("*year*", "%" + base.Random(1, (int)this.year).ToString() + "%"),
				Grammar.A(base.ExpandString("<spice.personNouns.!random>"), false),
				base.ExpandString("<spice.placeNouns.!random>"),
				snapVillage.GetProperty("name", "unknown"),
				randomProperty,
				base.ExpandString("<spice.temperaments." + randomElement + ".synonym.!random>"),
				Grammar.ObjectPronoun(Grammar.RandomShePronoun()),
				base.ExpandString("<spice.temperaments." + randomElement + ".action.!random>"),
				this.id
			}));
			base.AddEntityListItem("Gospels", text6, false);
			return;
			Block_19:
			string text7 = BecomesKnownFor.ItemCategories.GetRandomElement<string>(null);
			int num2 = 0;
			List<GameObjectBlueprint> blueprintsWithTag;
			do
			{
				blueprintsWithTag = GameObjectFactory.Factory.GetBlueprintsWithTag(text7, true);
				if (num2++ > 0)
				{
					text7 = BecomesKnownFor.ItemCategories.GetRandomElement<string>(null);
				}
			}
			while (blueprintsWithTag.Count == 0 && num2 < 50);
			num2 = 0;
			GameObjectBlueprint randomElement2;
			do
			{
				randomElement2 = blueprintsWithTag.GetRandomElement<GameObjectBlueprint>(null);
				num2++;
			}
			while ((randomElement2 == null || randomElement2.HasTag("ExcludeFromQuests", false)) && num2 < 50);
			string name = randomElement2.Name;
			GameObject gameObject = GameObject.Create(name);
			string text8 = base.ExpandString("<spice.villages.SignatureHistoricObject.!random>").Replace("*var*", gameObject.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, true, true, false, false, true, null, false));
			base.SetEntityProperty("signatureHistoricObjectType", name);
			base.SetEntityProperty("signatureHistoricObjectName", text8);
			base.AddEntityListItem("sacredThings", text8, false);
			base.AddEntityListItem("profaneThings", base.ExpandString("those who <spice.commonPhrases.profane.!random> the ") + text8, false);
			string text9 = Grammar.InitCap(string.Format("{0}, {1} gathered around {2} in {3} {4}. Thus the village of {5} was founded.|{6}", new object[]
			{
				base.ExpandString("<spice.commonPhrases.yearsAgo.!random>"),
				base.ExpandString("<spice.commonPhrases.people.!random>"),
				text8,
				base.ExpandString("<spice.commonPhrases.grave.!random>"),
				base.ExpandString("<spice.commonPhrases.reverence.!random>"),
				snapVillage.GetProperty("name", "unknown"),
				this.id
			}));
			base.AddEntityListItem("Gospels", text9, false);
		}

		// Token: 0x0400204D RID: 8269
		public static readonly string[] Temperaments = new string[] { "Angry" };

		// Token: 0x0400204E RID: 8270
		public static readonly string[] ItemCategories = new string[] { "MeleeWeapon", "MissileWeapon", "Armor", "Shield", "Grenade", "Preservable", "Book", "LightSource", "WaterContainer", "Furniture" };

		// Token: 0x0400204F RID: 8271
		public bool bVillageZero;
	}
}
