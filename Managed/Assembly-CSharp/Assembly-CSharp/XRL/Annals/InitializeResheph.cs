using System;
using HistoryKit;
using XRL.Language;

namespace XRL.Annals
{
	// Token: 0x0200060D RID: 1549
	[Serializable]
	public class InitializeResheph : HistoricEvent
	{
		// Token: 0x060041A9 RID: 16809 RVA: 0x001501F4 File Offset: 0x0014E3F4
		public InitializeResheph(int _period)
		{
			this.period = _period;
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x00150204 File Offset: 0x0014E404
		public override void Generate()
		{
			base.SetEntityProperty("type", "sultan");
			base.SetEntityProperty("Resheph", "true");
			base.SetEntityProperty("birthYear", this.year.ToString());
			base.AddEntityListItem("cognomen", "the Above", false);
			base.SetEntityProperty("cultName", "Cult of the Coiled Lamb");
			string text = "Resheph";
			base.SetEntityProperty("nameRoot", text);
			base.SetEntityProperty("suffix", "0");
			base.SetEntityProperty("name", text);
			base.SetEntityProperty("period", this.period.ToString());
			base.SetEntityProperty("isAlive", "true");
			string text2 = "he";
			base.SetEntityProperty("subjectPronoun", text2);
			base.SetEntityProperty("possessivePronoun", Grammar.PossessivePronoun(text2));
			base.SetEntityProperty("objectPronoun", Grammar.ObjectPronoun(text2));
			base.SetEntityProperty("region", "null");
			this.duration = 0L;
		}

		// Token: 0x04002049 RID: 8265
		public int period;
	}
}
