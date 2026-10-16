using System;
using HistoryKit;
using XRL.Language;

namespace XRL.Annals
{
	// Token: 0x02000600 RID: 1536
	[Serializable]
	public class FakedDeath : HistoricEvent
	{
		// Token: 0x0600417F RID: 16767 RVA: 0x0014C578 File Offset: 0x0014A778
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			base.SetEntityProperty("isAlive", "true");
			string text;
			if (snapshotAtYear.GetList("colors").Count == 0 || base.Random(0, 1) == 0)
			{
				text = "the " + base.ExpandString("<spice.elements." + snapshotAtYear.GetList("elements").GetRandomElement<string>(null) + ".adjectives.!random>") + " <spice.commonPhrases.ghost.!random>";
			}
			else
			{
				text = "the " + snapshotAtYear.GetList("colors").GetRandomElement<string>(null) + " <spice.commonPhrases.ghost.!random>";
			}
			if (50.in100())
			{
				text = text + " of " + snapshotAtYear.GetProperty("region", "unknown");
			}
			text = Grammar.MakeTitleCaseWithArticle(base.ExpandString(text));
			base.AddEntityListItem("cognomen", text, false);
			string text2 = base.ExpandString("<spice.instancesOf.backToLife.!random>").Replace("*var*", Grammar.MakePossessive(snapshotAtYear.GetProperty("name", "unknown")));
			string text3 = base.ExpandString("<spice.commonPhrases.itWasDiscovered.!random>");
			string text4 = string.Format("In %{0}%, {1} that {2}. Despite reports to the contrary, <entity.name> was alive and well. <entity.subjectPronoun.capitalize> was known thenceforth as {3}.", new object[]
			{
				this.year.ToString(),
				text3,
				text2,
				text
			});
			base.SetEventProperty("gospel", text4);
			if (text2.Contains("ntropic"))
			{
				base.SetEntityProperty("highlyEntropicBeingWorshipAttitude", "50");
			}
			string text5 = string.Format("{0}! {1} {2}, who in the {3} {4} death and {5} from the {6}.", new object[]
			{
				base.ExpandString("<spice.commonPhrases.onlooker.!random.capitalize>"),
				base.ExpandString("<spice.instancesOf.observeInWonder.!random.capitalize>"),
				text,
				QudHistoryHelpers.GenerateSultanateYearName(),
				base.ExpandString("<spice.commonPhrases.defied.!random>"),
				base.ExpandString("<spice.commonPhrases.emerged.!random>"),
				base.ExpandString(string.Concat(new string[]
				{
					"<spice.history.gospels.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".adjective.!random> <spice.history.gospels.",
					QudHistoryHelpers.GetSultanateEra(snapshotAtYear),
					".location.!random>"
				}))
			});
			base.SetEventProperty("tombInscription", text5);
			base.SetEventProperty("tombInscriptionCategory", "DoesSomethingRad");
		}
	}
}
