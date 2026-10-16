using System;
using HistoryKit;
using XRL.Rules;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x0200062C RID: 1580
	[Serializable]
	public class Worships : HistoricEvent
	{
		// Token: 0x060041F8 RID: 16888 RVA: 0x00154342 File Offset: 0x00152542
		public Worships()
		{
			this.bVillageZero = false;
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x00154351 File Offset: 0x00152551
		public Worships(bool VillageZero)
		{
			this.bVillageZero = VillageZero;
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x00154360 File Offset: 0x00152560
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			int num = base.Random(1, 3);
			string randomProperty = QudHistoryHelpers.GetRandomProperty(snapshotAtYear, snapshotAtYear.GetProperty("defaultSacredThing", "unknown"), new string[] { "sacredThings" });
			switch (num)
			{
			case 1:
			{
				HistoricEntitySnapshot currentSnapshot = Worships.GetSultanToWorshipByTier(snapshotAtYear.Tier, this.history).GetCurrentSnapshot();
				string text = currentSnapshot.GetProperty("name", "unknown");
				string text2 = ((currentSnapshot.GetList("cognomen").Count > 0) ? currentSnapshot.GetList("cognomen").GetRandomElement<string>(null) : null);
				base.SetEntityProperty("worships_sultan", text);
				base.SetEntityProperty("worships_sultan_id", currentSnapshot.entity.id);
				base.AddEntityListItem("itemAdjectiveRoots", text, false);
				base.AddEntityListItem("sacredThings", text, false);
				if (text2 != null)
				{
					text = (If.d100(30, null, null) ? (text + ", " + text2) : text);
				}
				base.AddEntityListItem("profaneThings", base.ExpandString("<spice.commonPhrases.profanity.!random> toward ") + text, false);
				string text3 = string.Format("The {0} of {1} was revealed to the people of {2} through {3}.|{4}", new object[]
				{
					base.ExpandString("<spice.instancesOf.divineFeeling.!random>"),
					text,
					snapshotAtYear.GetProperty("name", "unknown"),
					randomProperty,
					this.id
				});
				base.AddEntityListItem("Gospels", text3, false);
				return;
			}
			case 2:
			{
				string name = Factions.GetRandomFaction(snapshotAtYear.GetProperty("baseFaction", "unknown")).Name;
				string formattedName = Faction.GetFormattedName(name);
				base.AddEntityListItem("sacredThings", formattedName, false);
				base.AddEntityListItem("profaneThings", base.ExpandString("<spice.commonPhrases.profanity.!random> toward ") + formattedName, false);
				base.SetEntityProperty("worships_faction", name);
				base.AddEntityListItem("itemAdjectiveRoots", formattedName, false);
				string text3 = string.Format("The {0} of {1} was revealed to the people of {2} through {3}.|{4}", new object[]
				{
					base.ExpandString("<spice.instancesOf.divineFeeling.!random>"),
					formattedName,
					snapshotAtYear.GetProperty("name", "unknown"),
					randomProperty,
					this.id
				});
				base.AddEntityListItem("Gospels", text3, false);
				return;
			}
			case 3:
			{
				base.SetEntityProperty("worships_creature", "*Worships.LegendaryCreature*");
				base.AddEntityListItem("sacredThings", "*Worships.LegendaryCreature.DisplayName*", false);
				base.AddEntityListItem("profaneThings", base.ExpandString("<spice.commonPhrases.profanity.!random> toward *Worships.LegendaryCreature.DisplayName*"), false);
				base.AddEntityListItem("itemAdjectiveRoots", "*Worships.LegendaryCreature.ShortDisplayName*", false);
				string text3 = string.Format("The villagers of {0} laid offerings at the feet of {1} in exchange for {2} about {3}.|{4}", new object[]
				{
					snapshotAtYear.GetProperty("name", "unknown"),
					"*Worships.LegendaryCreature.DisplayName*",
					base.ExpandString("<spice.commonPhrases.wisdom.!random>"),
					randomProperty,
					this.id
				});
				base.AddEntityListItem("Gospels", text3, false);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x00154664 File Offset: 0x00152864
		public static HistoricEntity GetSultanToWorshipByTier(int tier, History history)
		{
			int basePeriod = 0;
			switch (tier)
			{
			case 0:
				basePeriod = 1;
				break;
			case 1:
				basePeriod = 1;
				break;
			case 2:
				basePeriod = 2;
				break;
			case 3:
				basePeriod = 2;
				break;
			case 4:
				basePeriod = 3;
				break;
			case 5:
				basePeriod = 3;
				break;
			case 6:
				basePeriod = 4;
				break;
			case 7:
				basePeriod = 4;
				break;
			case 8:
				basePeriod = 5;
				break;
			default:
				basePeriod = 1;
				break;
			}
			if (Stat.Random(0, 100) < 20)
			{
				if (If.CoinFlip(null, null))
				{
					basePeriod = Math.Max(1, basePeriod - 1);
				}
				else
				{
					basePeriod = Math.Min(5, basePeriod + 1);
				}
			}
			else if (Stat.Random(0, 100) < 10)
			{
				basePeriod = Stat.Random(1, 5);
			}
			return history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetCurrentSnapshot().GetProperty("type", "unknown") == "sultan" && entity.GetCurrentSnapshot().GetProperty("period", "unknown") == basePeriod.ToString()).GetRandomElement(null);
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x00154774 File Offset: 0x00152974
		public static void PostProcessEvent(HistoricEntity village, string creatureName, string creatureId)
		{
			village.SetEntityPropertyAtCurrentYear("worships_creature", creatureName);
			village.SetEntityPropertyAtCurrentYear("worships_creature_id", creatureId);
			village.MutateListPropertyAtCurrentYear("sacredThings", (string s) => s.Replace("*Worships.LegendaryCreature.DisplayName*", creatureName));
			village.MutateListPropertyAtCurrentYear("profaneThings", (string s) => s.Replace("*Worships.LegendaryCreature.DisplayName*", creatureName));
			village.MutateListPropertyAtCurrentYear("Gospels", (string s) => s.Replace("*Worships.LegendaryCreature.DisplayName*", creatureName));
			village.MutateListPropertyAtCurrentYear("itemAdjectiveRoots", (string s) => s.Replace("*Worships.LegendaryCreature.ShortDisplayName*", creatureName.Split(',', StringSplitOptions.None)[0]));
			village.SetEntityPropertyAtCurrentYear("proverb", village.GetCurrentSnapshot().GetProperty("proverb", "unknown").Replace("*Worships.LegendaryCreature.DisplayName*", creatureName));
			village.SetEntityPropertyAtCurrentYear("signatureDishName", village.GetCurrentSnapshot().GetProperty("signatureDishName", "unknown").Replace("*Worships.LegendaryCreature.DisplayName*", creatureName));
			village.SetEntityPropertyAtCurrentYear("newFactionName", village.GetCurrentSnapshot().GetProperty("newFactionName", "unknown").Replace("*Worships.LegendaryCreature.DisplayName*", creatureName));
		}

		// Token: 0x04002068 RID: 8296
		public const int CHANCE_FOR_NEARBY_SULTAN = 20;

		// Token: 0x04002069 RID: 8297
		public const int CHANCE_FOR_RANDOM_SULTAN = 10;

		// Token: 0x0400206A RID: 8298
		public const int CHANCE_TO_USE_COGNOMEN = 30;

		// Token: 0x0400206B RID: 8299
		public bool bVillageZero;
	}
}
