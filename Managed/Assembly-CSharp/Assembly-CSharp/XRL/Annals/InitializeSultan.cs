using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;
using XRL.Names;

namespace XRL.Annals
{
	// Token: 0x020005F9 RID: 1529
	[Serializable]
	public class InitializeSultan : HistoricEvent
	{
		// Token: 0x0600416C RID: 16748 RVA: 0x0014A835 File Offset: 0x00148A35
		public InitializeSultan(int _period)
		{
			this.period = _period;
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x0014A844 File Offset: 0x00148A44
		public override void Generate()
		{
			base.SetEntityProperty("type", "sultan");
			HistoricEntityList entitiesWherePropertyEquals = this.history.GetEntitiesWherePropertyEquals("type", "sultan");
			int num;
			string text;
			string text2;
			do
			{
				num = 0;
				bool flag = int.Parse(QudHistoryHelpers.GetRegionalizationParametersSnapshot(this.history).GetProperty("successorChance", "unknown")).in100();
				if (entitiesWherePropertyEquals.Count == 0)
				{
					flag = false;
				}
				if (flag)
				{
					HistoricEntity randomElement = entitiesWherePropertyEquals.GetRandomElement(null);
					text = randomElement.GetCurrentSnapshot().GetProperty("nameRoot", "unknown");
					int num2 = int.Parse(randomElement.GetCurrentSnapshot().GetProperty("suffix", "unknown"));
					if (num2 == 0)
					{
						num = 2;
						randomElement.ApplyEvent(new SetEntityProperties(new Dictionary<string, string>
						{
							{
								"suffix",
								1.ToString()
							},
							{
								"name",
								text + " I"
							}
						}, null), long.MinValue);
					}
					else
					{
						num = num2 + 1;
					}
				}
				else
				{
					do
					{
						text = NameMaker.MakeName(null, null, null, null, "Eater", null, null, null, null, null, null, null, null, false, false, null, null);
					}
					while (this.history.GetEntitiesWherePropertyEquals("name", text).Count > 0);
				}
				if (num == 0)
				{
					text2 = text;
				}
				else
				{
					text2 = text + " " + Grammar.GetRomanNumeral(num);
				}
			}
			while (this.history.GetEntitiesWherePropertyEquals("name", text2).Count != 0);
			base.SetEntityProperty("nameRoot", text);
			base.SetEntityProperty("suffix", num.ToString());
			base.SetEntityProperty("name", text2);
			base.SetEntityProperty("period", this.period.ToString());
			base.SetEntityProperty("isAlive", "true");
			string text3 = base.ExpandString("<spice.elements.!random>");
			base.AddEntityListItem("elements", text3, false);
			HistoricEntitySnapshot snapshotAtYear = this.history.GetEntitiesByDelegate((HistoricEntity entity) => entity.GetSnapshotAtYear(entity.lastYear).GetProperty("type", "unknown").Equals("region") && entity.GetSnapshotAtYear(entity.lastYear).GetProperty("period", "unknown") == this.period.ToString()).GetRandomElement(null).GetSnapshotAtYear(this.history.currentYear);
			base.SetEntityProperty("region", snapshotAtYear.GetProperty("name", "unknown"));
			base.SetEntityProperty("location", QudHistoryHelpers.GetRandomLocationInRegionByPeriod(this.history, snapshotAtYear.GetProperty("name", "unknown"), this.period));
			string text4 = Grammar.RandomShePronoun();
			base.SetEntityProperty("subjectPronoun", text4);
			base.SetEntityProperty("possessivePronoun", Grammar.PossessivePronoun(text4));
			base.SetEntityProperty("objectPronoun", Grammar.ObjectPronoun(text4));
			base.SetEntityProperty("reflexivePronoun", Grammar.ReflexivePronoun(text4));
			this.duration = 0L;
		}

		// Token: 0x0400201A RID: 8218
		public int period;
	}
}
