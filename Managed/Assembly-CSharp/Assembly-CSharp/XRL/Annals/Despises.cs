using System;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x02000624 RID: 1572
	[Serializable]
	public class Despises : HistoricEvent
	{
		// Token: 0x060041DC RID: 16860 RVA: 0x00151C80 File Offset: 0x0014FE80
		public Despises()
		{
			this.bVillageZero = false;
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x00151C8F File Offset: 0x0014FE8F
		public Despises(bool VillageZero)
		{
			this.bVillageZero = VillageZero;
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x00151CA0 File Offset: 0x0014FEA0
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			int num = base.Random(1, 3);
			string randomProperty = QudHistoryHelpers.GetRandomProperty(snapshotAtYear, snapshotAtYear.GetProperty("defaultProfaneThing", "unknown"), new string[] { "profaneThings" });
			switch (num)
			{
			case 1:
			{
				HistoricEntitySnapshot currentSnapshot = Worships.GetSultanToWorshipByTier(snapshotAtYear.Tier, this.history).GetCurrentSnapshot();
				string text = currentSnapshot.GetProperty("name", "unknown");
				string text2 = ((currentSnapshot.GetList("cognomen").Count > 0) ? currentSnapshot.GetList("cognomen").GetRandomElement<string>(null) : null);
				base.SetEntityProperty("despises_sultan", text);
				base.SetEntityProperty("despises_sultan_id", currentSnapshot.entity.id);
				if (text2 != null)
				{
					text = (If.d100(30, null, null) ? (text + ", " + text2) : text);
				}
				base.AddEntityListItem("profaneThings", text, false);
				string text3 = string.Format("The {0} of {1} was revealed to the people of {2} through {3}.|{4}", new object[]
				{
					base.ExpandString("<spice.instancesOf.profaneFeeling.!random>"),
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
				base.AddEntityListItem("profaneThings", formattedName, false);
				base.SetEntityProperty("despises_faction", name);
				string text3 = string.Format("The {0} of {1} was revealed to the people of {2} through {3}.|{4}", new object[]
				{
					base.ExpandString("<spice.instancesOf.profaneFeeling.!random>"),
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
				base.SetEntityProperty("despises_creature", "*Despises.LegendaryCreature*");
				base.AddEntityListItem("profaneThings", "*Despises.LegendaryCreature.DisplayName*", false);
				string text3 = Grammar.InitCap(string.Format("{0} {1}, who {2} {3}!|{4}", new object[]
				{
					base.ExpandString("<spice.instancesOf.aCurseUpon.!random>"),
					"*Despises.LegendaryCreature.DisplayName*",
					base.ExpandString("<spice.commonPhrases.supports.!random>"),
					randomProperty,
					this.id
				}));
				base.AddEntityListItem("Gospels", text3, false);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x00151F24 File Offset: 0x00150124
		public static void PostProcessEvent(HistoricEntity village, string creatureName, string creatureId)
		{
			village.SetEntityPropertyAtCurrentYear("despises_creature", creatureName);
			village.SetEntityPropertyAtCurrentYear("despises_creature_id", creatureId);
			village.MutateListPropertyAtCurrentYear("sacredThings", (string s) => s.Replace("*Despises.LegendaryCreature.DisplayName*", creatureName));
			village.MutateListPropertyAtCurrentYear("profaneThings", (string s) => s.Replace("*Despises.LegendaryCreature.DisplayName*", creatureName));
			village.MutateListPropertyAtCurrentYear("Gospels", (string s) => s.Replace("*Despises.LegendaryCreature.DisplayName*", creatureName));
			village.SetEntityPropertyAtCurrentYear("proverb", village.GetCurrentSnapshot().GetProperty("proverb", "unknown").Replace("*Despises.LegendaryCreature.DisplayName*", creatureName));
			village.SetEntityPropertyAtCurrentYear("signatureDishName", village.GetCurrentSnapshot().GetProperty("signatureDishName", "unknown").Replace("*Despises.LegendaryCreature.DisplayName*", creatureName));
			village.SetEntityPropertyAtCurrentYear("newFactionName", village.GetCurrentSnapshot().GetProperty("newFactionName", "unknown").Replace("*Despises.LegendaryCreature.DisplayName*", creatureName));
		}

		// Token: 0x04002050 RID: 8272
		public bool bVillageZero;
	}
}
