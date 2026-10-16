using System;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x020005FE RID: 1534
	[Serializable]
	public class ChariotDrivesOffCliff : HistoricEvent
	{
		// Token: 0x06004178 RID: 16760 RVA: 0x0014B9F8 File Offset: 0x00149BF8
		public ChariotDrivesOffCliff()
		{
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x0014BA00 File Offset: 0x00149C00
		public ChariotDrivesOffCliff(string regionToReveal)
		{
			this.regionToReveal = regionToReveal;
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x0014BA10 File Offset: 0x00149C10
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property;
			if (string.IsNullOrEmpty(this.regionToReveal))
			{
				property = snapshotAtYear.GetProperty("region", "unknown");
			}
			else
			{
				property = this.regionToReveal;
				base.SetEntityProperty("region", this.regionToReveal);
			}
			base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
			string text2;
			string text3;
			string text4;
			if (50.in100())
			{
				string text = ((snapshotAtYear.GetList("hatedFactions").Count != 0) ? snapshotAtYear.GetList("hatedFactions").GetRandomElement<string>(null) : QudHistoryHelpers.GetNewFaction(this.entity));
				if (snapshotAtYear.GetList("hatedFaction").Count == 0)
				{
					base.AddEntityListItem("hatedFaction", text, false);
				}
				text2 = Grammar.MakePossessive(snapshotAtYear.GetProperty("name", "unknown")) + " chariot was driven off a cliff by <spice.commonPhrases.rogue.!random.article> <spice.commonPhrases.band.!random> of " + Faction.GetFormattedGroupName(text);
				text3 = base.ExpandString(string.Concat(new string[]
				{
					"<spice.commonPhrases.band.!random.article> of treacherous ",
					Faction.GetFormattedGroupName(text),
					" <spice.history.gospels.VehicularSabotage.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".!random> in ",
					Grammar.MakePossessive(snapshotAtYear.GetProperty("name", "unknown")),
					" <spice.history.gospels.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".vehicle.!random>"
				})) + ", causing it to " + base.ExpandString("<spice.history.gospels.VehicularSabotageResult." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>");
				text4 = base.ExpandString(string.Concat(new string[]
				{
					"<spice.commonPhrases.band.!random.article> of treacherous ",
					Faction.GetFormattedGroupName(text),
					" <spice.history.gospels.VehicularSabotage.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".!random> in ",
					base.ExpandString("<entity.possessivePronoun>"),
					" <spice.history.gospels.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".vehicle.!random>"
				})) + ", causing it to " + base.ExpandString("<spice.history.gospels.VehicularSabotageResult." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>");
			}
			else
			{
				text2 = "<entity.name> lost control of <entity.possessivePronoun> chariot and drove it off a cliff";
				text3 = string.Concat(new string[]
				{
					"<entity.name> lost control of <entity.possessivePronoun> <spice.history.gospels.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".vehicle.!random> and <spice.history.gospels.CrashedVehicle.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".!random>"
				});
				text4 = string.Concat(new string[]
				{
					"<entity.subjectPronoun> lost control of <entity.possessivePronoun> <spice.history.gospels.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".vehicle.!random> and <spice.history.gospels.CrashedVehicle.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".!random>"
				});
			}
			string text6;
			if (base.Random(0, 1) == 0)
			{
				if (base.Random(0, 1) == 0)
				{
					string newFaction = QudHistoryHelpers.GetNewFaction(this.entity);
					base.AddEntityListItem("likedFactions", newFaction, false);
					string text5 = string.Format("{0} {7} {1}, {6}. {4}, a group of nearby {5} came to {3} rescue. For the rest of {3} life, {2} was indebted to {5}.", new object[]
					{
						Grammar.InitCap(base.ExpandString("<spice.commonPhrases.whileTraveling.!random>")),
						property,
						snapshotAtYear.GetProperty("name", "unknown"),
						Grammar.PossessivePronoun(snapshotAtYear.GetProperty("subjectPronoun", "unknown")),
						Grammar.InitCap(base.ExpandString("<spice.commonPhrases.luckily.!random>")),
						Faction.GetFormattedGroupName(newFaction),
						text2,
						"<spice.commonPhrases.in.!random>"
					});
					base.SetEventProperty("gospel", text5);
					text6 = string.Format("{0} the {1} {2} of {4}, who came to {3} rescue when {5}.", new object[]
					{
						base.ExpandString("<spice.instancesOf.bless.!random.capitalize>"),
						base.ExpandString("<spice.commonPhrases.noble.!random>"),
						Faction.GetFormattedGroupName(newFaction),
						Grammar.MakePossessive(snapshotAtYear.GetProperty("name", "unknown")),
						property,
						text4
					});
					base.SetEventProperty("tombInscriptionCategory", "DoesSomethingHumble");
				}
				else
				{
					string text7 = base.ExpandString("<spice.professions.!random>");
					base.SetEntityProperty("profession", text7);
					base.SetEntityProperty("professionRank", base.ExpandString("<spice.professions." + text7 + ".training>"));
					string text5 = string.Format("{0} {9} {1}, {6}. {4}, a group of nearby {5} came to {3} rescue. Moved by their kindness, {2} enrolled at a local {7} as a {8}.", new object[]
					{
						Grammar.InitCap(base.ExpandString("<spice.commonPhrases.whileTraveling.!random>")),
						property,
						snapshotAtYear.GetProperty("name", "unknown"),
						Grammar.PossessivePronoun(snapshotAtYear.GetProperty("subjectPronoun", "unknown")),
						Grammar.InitCap(base.ExpandString("<spice.commonPhrases.luckily.!random>")),
						base.ExpandString("<spice.professions." + text7 + ".plural>"),
						text2,
						base.ExpandString("<spice.professions." + text7 + ".guildhall>"),
						base.ExpandString("<spice.professions." + text7 + ".training>"),
						"<spice.commonPhrases.in.!random>"
					});
					base.SetEventProperty("gospel", text5);
					text6 = string.Format("{0} the {1} {2} of {4}, who came to {3} rescue when {5}.", new object[]
					{
						base.ExpandString("<spice.instancesOf.bless.!random.capitalize>"),
						base.ExpandString("<spice.commonPhrases.noble.!random>"),
						base.ExpandString("<spice.professions." + text7 + ".plural>"),
						Grammar.MakePossessive(snapshotAtYear.GetProperty("name", "unknown")),
						property,
						text4
					});
					base.SetEventProperty("tombInscriptionCategory", "DoesSomethingHumble");
				}
			}
			else
			{
				int num = (int)(this.year - this.entity.firstYear);
				base.SetEntityProperty("isAlive", "false");
				string text5 = string.Format("{0} {2} {1}, {3}. <entity.subjectPronoun.capitalize> was killed at {4} years old.", new object[]
				{
					Grammar.InitCap(base.ExpandString("<spice.commonPhrases.whileTraveling.!random>")),
					property,
					"<spice.commonPhrases.in.!random>",
					text2,
					num
				});
				base.SetEventProperty("gospel", text5);
				text6 = string.Format("In {0}, {1}. {2} was presumed dead at {3} years old.", new object[] { property, text3, "<entity.subjectPronoun.capitalize>", num });
				base.SetEventProperty("tombInscriptionCategory", "BodyExperienceBad");
			}
			base.SetEventProperty("tombInscription", text6);
		}

		// Token: 0x0400201C RID: 8220
		public string regionToReveal;
	}
}
