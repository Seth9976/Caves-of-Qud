using System;
using HistoryKit;
using XRL.Language;

namespace XRL.Annals
{
	// Token: 0x020005F5 RID: 1525
	[Serializable]
	public class Abdicate : HistoricEvent
	{
		// Token: 0x06004165 RID: 16741 RVA: 0x00149A30 File Offset: 0x00147C30
		public override void Generate()
		{
			this.duration = 0L;
			HistoricEntitySnapshot currentSnapshot = this.entity.GetCurrentSnapshot();
			string text2;
			string text3;
			if (!currentSnapshot.GetProperty("isSultan", "unknown").EqualsNoCase("true"))
			{
				base.SetEntityProperty("isSultan", "true");
				string text = base.ExpandString("<spice.instancesOf.sultanKnownFor.!random>");
				text2 = string.Format("{0} %{1}%, {2}, the sultan of Qud {3}. Because {4}, {5} was chosen as the successor.", new object[]
				{
					"<spice.instancesOf.inYear.!random.capitalize>",
					this.year,
					"<spice.instancesOf.afterTumultuousYears.!random>",
					"<spice.instancesOf.abdicated.!random>",
					base.ExpandString("<spice.instancesOf.ascensionReasons_VAR.!random>").Replace("*var*", Grammar.MakePossessive(currentSnapshot.GetProperty("name", "unknown"))).Replace("*var2*", text),
					"<entity.subjectPronoun>"
				});
				text3 = string.Format("{0} the {1} {2}, when {3} was dowered with the {4} and named sultan of Qud! {5} ancestors the Fossilized Saads looked on in approval, since -- just like them -- {6} was known by {8} {7}.", new object[]
				{
					"<spice.commonPhrases.celebrate.!random.capitalize>",
					"<spice.commonPhrases.sacred.!random>",
					QudHistoryHelpers.GenerateSultanateYearName(),
					"<entity.name>",
					QudHistoryHelpers.GetMaskName(currentSnapshot),
					"<entity.possessivePronoun.capitalize>",
					"<entity.subjectPronoun>",
					text,
					"<entity.possessivePronoun>"
				});
				base.SetEventProperty("tombInscriptionCategory", "CrownedSultan");
			}
			else
			{
				string text4 = base.ExpandString("<spice.instancesOf.afterTumultuousYears.!random>");
				string text5 = base.ExpandString("<spice.instancesOf.countered.!random>");
				text2 = string.Format("{0} %{1}%, {2}, {3} counselors suggested {4} {5}. Instead, {4} {6}.", new object[]
				{
					"<spice.instancesOf.inYear.!random.capitalize>",
					this.year,
					text4,
					Grammar.MakePossessive(base.ExpandString("<entity.name>")),
					"<entity.subjectPronoun>",
					"<spice.instancesOf.abdicate.!random>",
					text5
				});
				text3 = string.Format("{0} the lesson that {1} taught {2} advisors when they suggested {3} abdicate the throne {4}. Did {3} do any such thing? No! Instead, {3} {5}.", new object[] { "<spice.commonPhrases.remember.!random.capitalize>", "<entity.name>", "<entity.possessivePronoun>", "<entity.subjectPronoun>", text4, text5 });
				base.SetEventProperty("tombInscriptionCategory", "MeetsWithCounselors");
			}
			base.SetEventProperty("gospel", text2);
			base.SetEventProperty("tombInscription", text3);
		}
	}
}
