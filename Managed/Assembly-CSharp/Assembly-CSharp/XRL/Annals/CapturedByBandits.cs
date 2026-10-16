using System;
using HistoryKit;
using XRL.Language;

namespace XRL.Annals
{
	// Token: 0x020005FC RID: 1532
	[Serializable]
	public class CapturedByBandits : HistoricEvent
	{
		// Token: 0x06004175 RID: 16757 RVA: 0x0014B098 File Offset: 0x00149298
		public override void Generate()
		{
			this.duration = (long)base.Random(2, 8);
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property = snapshotAtYear.GetProperty("location", "unknown");
			string property2 = snapshotAtYear.GetProperty("region", "unknown");
			int num = base.Random(0, 2);
			int num2 = base.Random(0, 1);
			string text = null;
			string text2;
			if (num == 0)
			{
				text2 = string.Concat(new string[] { "While traveling near ", property, " in ", property2, ", " });
				base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property2));
			}
			else if (num == 1)
			{
				text2 = "On an expedition around " + property2 + ", ";
				base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property2));
			}
			else
			{
				text2 = "Near the location of " + property + ", ";
			}
			if (num2 == 0)
			{
				string text3 = snapshotAtYear.GetList("elements")[base.Random(0, snapshotAtYear.GetList("elements").Count - 1)];
				string text4 = Grammar.InitCap(Grammar.MakeCompoundWord(Grammar.GetRandomMeaningfulWord(base.ExpandString("<spice.elements." + text3 + ".nouns.!random.capitalize>")), base.ExpandString("<spice.actornouns.!random>"), false));
				base.AddEntityListItem("cognomen", text4, false);
				string text5 = text2 + base.ExpandString("<entity.name> was captured by bandits. <entity.subjectPronoun.capitalize> murdered their leader <spice.elements.entity$elements[random].murdermethods.!random>, and was thenceforth known as " + text4 + ".");
				base.SetEventProperty("gospel", text5);
				if (text5.Contains("entropy"))
				{
					base.SetEntityProperty("highlyEntropicBeingWorshipAttitude", "-50");
				}
				text = string.Format("{0} the {1} of {2} {3}! " + text5, new object[]
				{
					base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
					base.ExpandString("<spice.commonPhrases.fate.!random>"),
					Grammar.MakePossessive(snapshotAtYear.GetProperty("name", "unknown")),
					base.ExpandString("<spice.commonPhrases.foes.!random>")
				});
				base.SetEventProperty("tombInscriptionCategory", "Slays");
			}
			else if (num2 == 1)
			{
				string newLocationInRegion = QudHistoryHelpers.GetNewLocationInRegion(this.history, property2, property);
				string text6 = base.ExpandString("<spice.history.gospels.HumblePractice." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>");
				base.SetEntityProperty("location", newLocationInRegion);
				base.SetEventProperty("gospel", string.Concat(new string[]
				{
					text2,
					"<entity.name> was captured by bandits. <entity.subjectPronoun.capitalize> languished in captivity for ",
					Grammar.Cardinal((int)this.duration),
					" years, eventually escaping to ",
					newLocationInRegion,
					"."
				}));
				text = string.Format("For the sake of {12}, {0} {1} at {2} in {3} to {4} among the {5} and {6}. After {7} {8} years, {9} {10} at {11}.", new object[]
				{
					"<entity.name>",
					base.ExpandString("<spice.commonPhrases.settled.!random>"),
					property,
					property2,
					base.ExpandString("<spice.instancesOf.dwellOrWork.!random>"),
					base.ExpandString("<spice.instancesOf.commonFolk.!random>"),
					text6,
					Grammar.Cardinal((int)this.duration),
					base.ExpandString("<spice.commonPhrases.humble.!random>"),
					"<entity.subjectPronoun>",
					base.ExpandString("<spice.instancesOf.reemerged.!random>"),
					newLocationInRegion,
					base.ExpandString("<spice.commonPhrases.wisdom.!random>")
				});
				base.SetEventProperty("tombInscriptionCategory", "EnduresHardship");
			}
			base.SetEventProperty("tombInscription", text);
		}
	}
}
