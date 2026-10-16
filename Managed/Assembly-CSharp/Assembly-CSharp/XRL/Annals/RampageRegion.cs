using System;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x0200060C RID: 1548
	[Serializable]
	public class RampageRegion : HistoricEvent
	{
		// Token: 0x060041A6 RID: 16806 RVA: 0x0014FEF7 File Offset: 0x0014E0F7
		public RampageRegion()
		{
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x0014FEFF File Offset: 0x0014E0FF
		public RampageRegion(string _regionToReveal)
		{
			this.regionToReveal = _regionToReveal;
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x0014FF10 File Offset: 0x0014E110
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property = snapshotAtYear.GetProperty("region", "unknown");
			string newRegion;
			if (string.IsNullOrEmpty(this.regionToReveal))
			{
				newRegion = QudHistoryHelpers.GetNewRegion(this.history, property);
			}
			else
			{
				newRegion = this.regionToReveal;
			}
			base.SetEntityProperty("region", newRegion);
			base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, newRegion));
			string newFaction = QudHistoryHelpers.GetNewFaction(this.entity);
			string text = QudHistoryHelpers.GetNewFaction(this.entity);
			while (newFaction.Equals(text))
			{
				text = QudHistoryHelpers.GetNewFaction(this.entity);
			}
			base.AddEntityListItem("hatedFactions", newFaction, false);
			base.AddEntityListItem("hatedFactions", text, false);
			string text2;
			if (base.Random(0, 1) == 0)
			{
				text2 = base.ExpandString("<spice.commonPhrases.scourge.!random> of " + QudHistoryHelpers.GetRegionNameRoot(this.history, newRegion));
				if (snapshotAtYear.GetList("colors").Count != 0)
				{
					text2 = snapshotAtYear.GetList("colors").GetRandomElement<string>(null) + " " + text2;
				}
				text2 = Grammar.MakeTitleCaseWithArticle("the " + text2);
			}
			else
			{
				text2 = Grammar.MakeTitleCaseWithArticle(base.ExpandString("the " + QudHistoryHelpers.GetRegionNameRoot(this.history, newRegion) + " <spice.commonPhrases.scourge.!random>"));
			}
			text2 = text2.Replace("the the", "the");
			base.AddEntityListItem("cognomen", text2, false);
			base.SetEventProperty("gospel", string.Concat(new string[]
			{
				"<spice.commonPhrases.allThroughout.!random.capitalize> %",
				this.year.ToString(),
				"%, <entity.name> <spice.commonPhrases.ravaged.!random> all of ",
				newRegion,
				", <spice.elements.entity$elements[random].ravaging.!random> of ",
				Faction.GetFormattedName(newFaction),
				" and ",
				Faction.GetFormattedName(text),
				". <entity.subjectPronoun.capitalize> became known as ",
				text2,
				"."
			}));
			string randomCognomen = QudHistoryHelpers.GetRandomCognomen(snapshotAtYear);
			if (!string.Equals(randomCognomen, "the Untitled") && 30.in100())
			{
				text2 = randomCognomen;
			}
			string text3 = string.Format("{7}, {8}! In the {0}, a {1} of {2} and {3} {4} {5} and {6}. {7} the {9} put upon them by {10}, {11}!", new object[]
			{
				QudHistoryHelpers.GenerateSultanateYearName(),
				base.ExpandString("<spice.commonPhrases.coalition.!random>"),
				Faction.GetFormattedName(newFaction),
				Faction.GetFormattedName(text),
				base.ExpandString("<spice.instancesOf.brokeFaithWith.!random>"),
				"<entity.name>",
				base.ExpandString("<spice.history.gospels.CommittedWrongAgainstSultan." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
				base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
				base.ExpandString("<spice.instancesOf.yeGodless.!random>"),
				base.ExpandString("<spice.commonPhrases.chastisement.!random>"),
				"<entity.name>",
				text2
			});
			base.SetEventProperty("tombInscription", text3);
			base.SetEventProperty("tombInscriptionCategory", "DoesSomethingDestructive");
		}

		// Token: 0x04002047 RID: 8263
		public string regionToReveal;

		// Token: 0x04002048 RID: 8264
		public const int CHANCE_USE_OLD_COGNOMEN = 30;
	}
}
