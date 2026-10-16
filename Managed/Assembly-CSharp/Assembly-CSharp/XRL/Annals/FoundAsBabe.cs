using System;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x020005F8 RID: 1528
	[Serializable]
	public class FoundAsBabe : HistoricEvent
	{
		// Token: 0x0600416B RID: 16747 RVA: 0x0014A510 File Offset: 0x00148710
		public override void Generate()
		{
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property = snapshotAtYear.GetProperty("name", "unknown");
			string randomElement = snapshotAtYear.GetList("elements").GetRandomElement<string>(null);
			string property2 = snapshotAtYear.GetProperty("subjectPronoun", "unknown");
			string property3 = snapshotAtYear.GetProperty("region", "unknown");
			int num = base.Random(0, 2);
			string text = "<spice.commonPhrases.oneStarryNight.!random.capitalize>";
			string text2;
			if (num == 0)
			{
				text2 = string.Concat(new string[] { text, ", a babe was found swaddled <spice.myth.mythicPrepPlaces.!random> <spice.elements.", randomElement, ".babeTrait.!random>. That babe came to be known as ", property, "." });
				base.SetEventProperty("gospel", text2);
			}
			else if (num == 1)
			{
				string text3 = base.ExpandString("<spice.elements." + randomElement + ".professions.!random>");
				base.SetEntityProperty("profession", text3);
				base.SetEntityProperty("professionRank", base.ExpandString("<spice.professions." + text3 + ".training>"));
				string text4 = Grammar.RandomShePronoun();
				text2 = string.Concat(new string[]
				{
					text,
					", a <spice.professions.",
					text3,
					".singular> found a babe <spice.elements.",
					randomElement,
					".babeTrait.!random> outside ",
					Grammar.PossessivePronoun(text4),
					" <spice.professions.",
					text3,
					".guildhall>. ",
					Grammar.InitCap(text4),
					" and ",
					Grammar.PossessivePronoun(text4),
					" fellow <spice.professions.",
					text3,
					".plural> adopted the babe and named ",
					Grammar.ObjectPronoun(property2),
					" ",
					property,
					"."
				});
				base.SetEventProperty("gospel", text2);
			}
			else
			{
				string newFaction = QudHistoryHelpers.GetNewFaction(this.entity);
				base.AddEntityListItem("likedFactions", newFaction, false);
				string text5 = "";
				if (base.Random(0, 1) == 0)
				{
					text5 += "the ";
				}
				text5 += base.ExpandString("<spice.elements." + randomElement + ".adjectives.!random.capitalize> <spice.commonPhrases.scion.!random.capitalize> of " + Grammar.MakeTitleCaseWithArticle(Faction.GetFormattedName(newFaction)));
				base.AddEntityListItem("cognomen", text5, false);
				text2 = string.Concat(new string[]
				{
					text,
					", a babe was found swaddled <spice.elements.",
					randomElement,
					".babeTrait.!random> by a group of ",
					Faction.GetFormattedName(newFaction),
					" in ",
					property3,
					". They took ",
					Grammar.ObjectPronoun(property2),
					" into their fold and fostered ",
					Grammar.ObjectPronoun(property2),
					", and ",
					property2,
					" became known as ",
					property,
					", ",
					text5,
					"."
				});
				base.SetEventProperty("gospel", text2);
				base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property3));
			}
			base.SetEventProperty("tombInscription", text2);
			base.SetEventProperty("tombInscriptionCategory", "IsBorn");
			this.duration = 0L;
		}
	}
}
