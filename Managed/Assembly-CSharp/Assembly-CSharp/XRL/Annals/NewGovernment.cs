using System;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.Names;
using XRL.World;
using XRL.World.ZoneBuilders;

namespace XRL.Annals
{
	// Token: 0x02000628 RID: 1576
	[Serializable]
	public class NewGovernment : HistoricEvent
	{
		// Token: 0x060041EB RID: 16875 RVA: 0x00152A8B File Offset: 0x00150C8B
		public NewGovernment()
		{
			this.bVillageZero = false;
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x00152A9A File Offset: 0x00150C9A
		public NewGovernment(bool VillageZero)
		{
			this.bVillageZero = VillageZero;
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x00152AAC File Offset: 0x00150CAC
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			int num2;
			for (;;)
			{
				int num = base.Random(1, 1000);
				num2 = 0;
				foreach (string text in snapshotAtYear.GetList("removeIfGovtChanges_sacredThings"))
				{
					base.RemoveEntityListItem("sacredThings", text);
				}
				foreach (string text2 in snapshotAtYear.GetList("removeIfGovtChanges_profaneThings"))
				{
					base.RemoveEntityListItem("profaneThings", text2);
				}
				if (If.Chance(50, null, null))
				{
					foreach (string text3 in snapshotAtYear.GetList("removeIfGovtChanges_sacredThings"))
					{
						base.AddEntityListItem("profaneThings", text3, false);
					}
					foreach (string text4 in snapshotAtYear.GetList("removeIfGovtChanges_profaneThings"))
					{
						base.AddEntityListItem("sacredThings", text4, false);
					}
				}
				foreach (string text5 in snapshotAtYear.GetList("removeIfGovtChanges_sacredThings"))
				{
					base.RemoveEntityListItem("removeIfGovtChanges_sacredThings", text5);
				}
				foreach (string text6 in snapshotAtYear.GetList("removeIfGovtChanges_profaneThings"))
				{
					base.RemoveEntityListItem("removeIfGovtChanges_profaneThings", text6);
				}
				this.ApplyToSnapshot(snapshotAtYear);
				if (num <= 200)
				{
					break;
				}
				num2 += 200;
				if (num <= num2 + 200)
				{
					goto Block_12;
				}
				num2 += 200;
				if (num <= num2 + 200)
				{
					goto Block_13;
				}
				num2 += 200;
				if (num <= num2 + 200)
				{
					goto Block_14;
				}
				num2 += 200;
				if (num > num2 + 200)
				{
					goto IL_0A30;
				}
				if (!this.bVillageZero)
				{
					goto Block_19;
				}
			}
			string text7 = (If.CoinFlip(null, null) ? "king" : "queen");
			base.SetEntityProperty("government", "monarchy");
			base.SetEntityProperty("mayorTemplate", Grammar.InitCap(text7));
			string property = snapshotAtYear.GetProperty("region", "unknown");
			string text8 = NameMaker.MakeName(VillageBase.getBaseVillager(snapshotAtYear.GetProperty("baseFaction", "unknown"), property, "10", "50", false) ?? GameObject.Create(PopulationManager.RollOneFrom("LairOwners_" + property, null, null).Blueprint), null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
			text8 = (If.CoinFlip(null, null) ? ("King " + text8 + " I") : ("Queen " + text8 + " I"));
			string text9 = Grammar.InitCap(string.Format("{0} the memory of {1}, who founded the monarchy of {2} for {3}.|{4}", new object[]
			{
				base.ExpandString("<spice.commonPhrases.love.!random>"),
				text8,
				snapshotAtYear.GetProperty("name", "unknown"),
				snapshotAtYear.sacredThing,
				this.id
			}));
			base.AddEntityListItem("Gospels", text9, false);
			base.AddEntityListItem("sacredThings", "the " + text7, false);
			base.AddEntityListItem("removeIfGovtChanges_sacredThings", "the " + text7, false);
			string text10 = "those who " + base.ExpandString("<spice.commonPhrases.profane.!random> the ") + text7;
			base.AddEntityListItem("removeIfGovtChanges_profaneThings", text10, false);
			base.AddEntityListItem("profaneThings", text10, false);
			base.SetEntityProperty("governor", "the " + text7);
			return;
			Block_12:
			string text11 = Grammar.InitCap(string.Format("In %{0}%, the villagers of {1} demanded that {2} {3} so the people could govern themselves. The two pillars of {4} in {1} thus became democracy and {5}.|{6}", new object[]
			{
				this.year,
				snapshotAtYear.Name,
				snapshotAtYear.GetProperty("governor", "unknown"),
				base.ExpandString("<spice.instancesOf.stepDown.!random>"),
				base.ExpandString("<spice.commonPhrases.society.!random>"),
				snapshotAtYear.sacredThing,
				this.id
			}));
			base.SetEntityProperty("mayorTemplate", "VillagerMayor");
			base.SetEntityProperty("government", "direct democracy");
			base.AddEntityListItem("Gospels", text11, false);
			base.AddEntityListItem("sacredThings", "democracy", false);
			base.AddEntityListItem("sacredThings", "the people", false);
			base.AddEntityListItem("removeIfGovtChanges_sacredThings", "democracy", false);
			base.AddEntityListItem("removeIfGovtChanges_sacredThings", "the people", false);
			string text12 = "those who " + base.ExpandString("<spice.commonPhrases.profane.!random> the ") + "democracy";
			base.AddEntityListItem("profaneThings", text12, false);
			base.AddEntityListItem("removeIfGovtChanges_profaneThings", text12, false);
			base.SetEntityProperty("governor", "the people");
			return;
			Block_13:
			string text13 = base.ExpandString("<spice.villages.government.representativeDemocracy.!random>");
			string text14 = base.ExpandString("<spice.villages.government.representativeDemocracy." + text13 + ".governor.!random>");
			string text15 = base.ExpandString("<spice.villages.government.representativeDemocracy." + text13 + ".mayorTemplate.!random>");
			string text16 = Grammar.InitCap(string.Format("In %{0}%, the villagers of {1} deposed {2} and {3} power to an elected {4}. The {5} promised to forever uphold the {6} of {7}.|{8}", new object[]
			{
				this.year,
				snapshotAtYear.Name,
				snapshotAtYear.GetProperty("governor", "unknown"),
				base.ExpandString("<spice.commonPhrases.bequeathed.!random>"),
				text13,
				Grammar.InitLower(Grammar.Pluralize(text15)),
				base.ExpandString("<spice.commonPhrases.sanctity.!random>"),
				snapshotAtYear.sacredThing,
				this.id
			}));
			base.SetEntityProperty("mayorTemplate", text15);
			base.SetEntityProperty("government", "representative democracy");
			base.AddEntityListItem("Gospels", text16, false);
			base.AddEntityListItem("sacredThings", "democracy", false);
			base.AddEntityListItem("sacredThings", text14, false);
			base.AddEntityListItem("removeIfGovtChanges_sacredThings", "democracy", false);
			base.AddEntityListItem("removeIfGovtChanges_sacredThings", text14, false);
			string text17 = "those who " + base.ExpandString("<spice.commonPhrases.profane.!random> ") + text14;
			base.AddEntityListItem("profaneThings", text17, false);
			base.AddEntityListItem("removeIfGovtChanges_profaneThings", text17, false);
			base.SetEntityProperty("governor", text14);
			return;
			Block_14:
			string anonLegendaryCreatureBlueprint = EncountersAPI.GetANonLegendaryCreatureBlueprint((GameObjectBlueprint b) => !b.HasTag("Merchant", false) && !b.HasTag("ExcludeFromVillagePopulations", false) && !b.HasProperName() && b.HasPart("Combat"));
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(anonLegendaryCreatureBlueprint);
			string text18 = "the " + gameObject.DisplayNameOnlyDirectAndStripped + " colonists";
			string text19 = (If.CoinFlip(null, null) ? "Viceroy" : "Governor");
			string text20;
			if (If.CoinFlip(null, null))
			{
				string sacredThing = snapshotAtYear.sacredThing;
				text20 = Grammar.InitCap(string.Format("In %{0}%, {1} settled in {2}, unseating {3} and imposing {4} that {5} {6}.|{7}", new object[]
				{
					this.year,
					Grammar.Pluralize(gameObject.DisplayNameOnlyDirectAndStripped),
					snapshotAtYear.Name,
					snapshotAtYear.GetProperty("governor", "unknown"),
					base.ExpandString("<spice.commonPhrases.laws.!random>"),
					base.ExpandString("<spice.commonPhrases.prohibited.!random>"),
					snapshotAtYear.sacredThing,
					this.id
				}));
				base.RemoveEntityListItem("sacredThings", sacredThing);
				base.AddEntityListItem("profaneThings", sacredThing, false);
				base.AddEntityListItem("removeIfGovtChanges_profaneThings", sacredThing, false);
			}
			else
			{
				string profaneThing = snapshotAtYear.profaneThing;
				text20 = Grammar.InitCap(string.Format("In %{0}%, {1} settled in {2}, unseating {3} and imposing {4} in {5} of {6}.|{7}", new object[]
				{
					this.year,
					Grammar.Pluralize(gameObject.DisplayNameOnlyDirectAndStripped),
					snapshotAtYear.Name,
					snapshotAtYear.GetProperty("governor", "unknown"),
					base.ExpandString("<spice.commonPhrases.laws.!random>"),
					base.ExpandString("<spice.commonPhrases.protection.!random>"),
					snapshotAtYear.profaneThing,
					this.id
				}));
				base.RemoveEntityListItem("profaneThings", profaneThing);
				base.AddEntityListItem("sacredThings", profaneThing, false);
				base.AddEntityListItem("removeIfGovtChanges_sacredThings", profaneThing, false);
			}
			base.SetEntityProperty("mayorTemplate", text19);
			base.SetEntityProperty("government", "colonialism");
			base.SetEntityProperty("colonistType", anonLegendaryCreatureBlueprint);
			base.SetEntityProperty("governor", text18);
			base.AddEntityListItem("Gospels", text20, false);
			base.AddEntityListItem("sacredThings", text18, false);
			base.AddEntityListItem("removeIfGovtChanges_sacredThings", text18, false);
			string text21 = "those who " + base.ExpandString("<spice.commonPhrases.profane.!random> ") + text18;
			base.AddEntityListItem("profaneThings", text21, false);
			base.AddEntityListItem("removeIfGovtChanges_profaneThings", text21, false);
			return;
			Block_19:
			string text22 = Grammar.InitCap(string.Format("In %{0}%, the villagers of {1} demanded that {2} {3} and all forms of hierarchy be abolished from their village. The two pillars of {4} in {1} thus became anarchy and {5}.|{6}", new object[]
			{
				this.year,
				snapshotAtYear.Name,
				snapshotAtYear.GetProperty("governor", "unknown"),
				base.ExpandString("<spice.instancesOf.stepDown.!random>"),
				base.ExpandString("<spice.commonPhrases.society.!random>"),
				snapshotAtYear.sacredThing,
				this.id
			}));
			base.SetEntityProperty("mayorTemplate", "VillagerMayor");
			base.SetEntityProperty("government", "anarchism");
			base.AddEntityListItem("Gospels", text22, false);
			base.AddEntityListItem("sacredThings", "anarchy", false);
			base.AddEntityListItem("sacredThings", "the abolition of hierarchy", false);
			base.AddEntityListItem("removeIfGovtChanges_sacredThings", "anarchy", false);
			base.AddEntityListItem("removeIfGovtChanges_sacredThings", "the abolition of hierarchy", false);
			string text23 = "hierarchy";
			base.AddEntityListItem("profaneThings", text23, false);
			base.AddEntityListItem("removeIfGovtChanges_profaneThings", text23, false);
			base.SetEntityProperty("governor", "the people");
			return;
			IL_0A30:
			num2 += 200;
		}

		// Token: 0x0400205A RID: 8282
		public bool bVillageZero;
	}
}
