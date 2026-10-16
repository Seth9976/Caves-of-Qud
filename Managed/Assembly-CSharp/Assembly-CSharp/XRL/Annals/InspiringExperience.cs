using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000604 RID: 1540
	[Serializable]
	public class InspiringExperience : HistoricEvent
	{
		// Token: 0x06004187 RID: 16775 RVA: 0x0014D170 File Offset: 0x0014B370
		public override void Generate()
		{
			this.duration = 1L;
			string text = null;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string text2 = base.ExpandString("<spice.elements.!random>");
			while (snapshotAtYear.GetList("elements").Contains(text2))
			{
				text2 = base.ExpandString("<spice.elements.!random>");
			}
			base.AddEntityListItem("elements", text2, false);
			string property = snapshotAtYear.GetProperty("region", "unknown");
			int num = base.Random(0, 2);
			int num2 = base.Random(0, 1);
			string text3;
			if (num == 0)
			{
				text3 = "Somewhere in " + property + ", ";
				base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
			}
			else if (num == 1)
			{
				text3 = "While traveling alone in " + property + ", ";
				base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
			}
			else
			{
				text3 = "One auspicious day, ";
			}
			if (num2 == 0)
			{
				text = base.ExpandString(string.Concat(new string[] { text3, "<entity.name> <spice.elements.", text2, ".inspirationsVerbPhrase.!random>. <spice.commonPhrases.fromThenOn.!random.capitalize>, <entity.subjectPronoun> was obsessed with <spice.elements.", text2, ".nounsPlural.!random>." }));
				base.SetEventProperty("gospel", text);
			}
			else if (num2 == 1)
			{
				text = base.ExpandString(string.Concat(new string[] { text3, "<entity.name> <spice.elements.", text2, ".inspirationsVerbPhrase.!random>. <spice.commonPhrases.fromThenOn.!random.capitalize>, <entity.subjectPronoun> always kept some <spice.elements.", text2, ".nounsPlural.!random> hidden on <entity.possessivePronoun> person." }));
				base.SetEventProperty("gospel", text);
			}
			base.SetEventProperty("tombInscription", text);
			base.SetEventProperty("tombInscriptionCategory", "HasInspiringExperience");
		}
	}
}
