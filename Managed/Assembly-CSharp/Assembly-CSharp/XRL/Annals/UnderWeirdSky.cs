using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;

namespace XRL.Annals
{
	// Token: 0x0200061F RID: 1567
	[Serializable]
	public class UnderWeirdSky : HistoricEvent
	{
		// Token: 0x060041CF RID: 16847 RVA: 0x00150B00 File Offset: 0x0014ED00
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string randomElement = snapshotAtYear.GetList("elements").GetRandomElement<string>(null);
			string text = base.ExpandString("<spice.elements." + randomElement + ".nouns.!random>");
			string property = snapshotAtYear.GetProperty("region", "unknown");
			string text2 = snapshotAtYear.GetProperty("location", "unknown");
			text2 = QudHistoryHelpers.GetNewLocationInRegion(this.history, property, text2);
			base.SetEntityProperty("location", text2);
			string text3 = base.ExpandString("<spice.colors.!random>");
			base.AddEntityListItem("colors", text3, false);
			string text4;
			if (base.Random(0, 1) == 0)
			{
				text4 = Grammar.MakeTitleCaseWithArticle("the " + text3 + " " + text);
			}
			else
			{
				text4 = string.Join("-", Grammar.MakeTitleCaseWithArticle(Grammar.GetRandomMeaningfulWord(text) + " in " + text3).Split(' ', StringSplitOptions.None));
			}
			base.AddEntityListItem("cognomen", text4, false);
			string text5 = string.Concat(new string[]
			{
				"At <spice.time.partsOfDay.!random> under <spice.commonPhrases.strange.!random.article> and ",
				text3,
				" sky, the people of ",
				text2,
				" saw an image on the horizon that looked like ",
				Grammar.A(text, false),
				" bathed in ",
				text3,
				". It was <entity.name>, and after <entity.subjectPronoun> came and left ",
				text2,
				", the people built a monument to <entity.objectPronoun>, and thenceforth called <entity.objectPronoun> ",
				text4,
				"."
			});
			base.SetEventProperty("gospel", text5);
			base.SetEventProperty("tombInscription", text5);
			base.SetEventProperty("tombInscriptionCategory", "DoesSomethingRad");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("monuments", snapshotAtYear.GetProperty("name", "unknown"));
			this.history.GetEntitiesWherePropertyEquals("name", text2).GetRandomElement(null).ApplyEvent(new SetEntityProperties(null, dictionary), long.MinValue);
		}
	}
}
