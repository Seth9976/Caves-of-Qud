using System;
using System.Text.RegularExpressions;
using HistoryKit;
using XRL.Language;
using XRL.Names;

namespace XRL.Annals
{
	// Token: 0x020005F7 RID: 1527
	[Serializable]
	public class BornAsHeir : HistoricEvent
	{
		// Token: 0x06004169 RID: 16745 RVA: 0x0014A274 File Offset: 0x00148474
		public override void Generate()
		{
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property = snapshotAtYear.GetProperty("name", "unknown");
			string randomElement = snapshotAtYear.GetList("elements").GetRandomElement<string>(null);
			string property2 = snapshotAtYear.GetProperty("subjectPronoun", "unknown");
			string property3 = snapshotAtYear.GetProperty("region", "unknown");
			string property4 = snapshotAtYear.GetProperty("location", "unknown");
			string text = NameMaker.MakeName(null, null, null, null, "Eater", null, null, null, null, null, null, null, null, false, false, null, null);
			string text2 = "<spice.commonPhrases.oneStarryNight.!random.capitalize>";
			string text3 = base.ExpandString("<spice.elements." + randomElement + ".nouns.!random>");
			string text4 = base.ExpandString("<spice.elements." + randomElement + ".mythicalEvent.!random>").Replace("*var*", text3);
			Match match = Regex.Match(text4, "a famous \\w*");
			string text5;
			if (match.Success)
			{
				text5 = text4.Replace(match.Groups[0].Value, text);
			}
			else
			{
				text5 = text4;
			}
			string text6;
			if (base.Random(1, 4) < 3)
			{
				text6 = Grammar.MakeTitleCaseWithArticle("the " + base.ExpandString("<spice.commonPhrases.eminent.!random> ") + text3);
			}
			else
			{
				text6 = Grammar.MakeTitleCase(Grammar.GetRandomMeaningfulWord(text3) + "born");
			}
			string text7 = string.Format("{0} in {1}, a healthy child was born to the sultan at {2}. At the moment of {3} birth, {4}, and in celebration the people {5}. The babe was named {6}, but the people called {7} {8}.", new object[]
			{
				text2,
				property3,
				property4,
				Grammar.PossessivePronoun(property2),
				text4,
				base.ExpandString("<spice.commonPhrases.celebrated.!random>"),
				property,
				Grammar.ObjectPronoun(property2),
				text6
			});
			base.SetEventProperty("gospel", text7);
			base.AddEntityListItem("cognomen", text6, false);
			base.SetEntityProperty("isHeir", "true");
			string text8 = ((snapshotAtYear.GetProperty("subjectPronoun", "unknown") == "he") ? "heir" : "heiress");
			string text9 = string.Format("{0}! {1} the life of {2} -- called {3}! -- scion of the Fossilized Saads and {4} who was dowered the {5}. Observe the twin miracles of the {6}: the {7} was born, and {8}.", new object[]
			{
				base.ExpandString("<spice.commonPhrases.onlooker.!random.capitalize>"),
				base.ExpandString("<spice.instancesOf.observeInWonder.!random.capitalize>"),
				"<entity.name>",
				text6,
				"<entity.subjectPronoun>",
				QudHistoryHelpers.GetMaskName(snapshotAtYear),
				QudHistoryHelpers.GenerateSultanateYearName(),
				text8,
				text5
			});
			base.SetEventProperty("tombInscription", text9);
			base.SetEventProperty("tombInscriptionCategory", "IsBorn");
			this.duration = 0L;
		}
	}
}
