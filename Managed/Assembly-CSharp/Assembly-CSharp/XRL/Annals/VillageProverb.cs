using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;

namespace XRL.Annals
{
	// Token: 0x0200062B RID: 1579
	[Serializable]
	public class VillageProverb : HistoricEvent
	{
		// Token: 0x060041F6 RID: 16886 RVA: 0x0015422C File Offset: 0x0015242C
		public VillageProverb(string SpiceElement = "proverbs", string SacredThing = null, string ProfaneThing = null, int Chance = 50)
		{
			this.SpiceElement = SpiceElement;
			this.SacredThing = SacredThing;
			this.ProfaneThing = ProfaneThing;
			this.Chance = Chance;
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x00154254 File Offset: 0x00152454
		public override void Generate()
		{
			this.duration = 0L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			if (snapshotAtYear == null)
			{
				throw new Exception("Cannot load snapshot for year " + this.entity.lastYear.ToString());
			}
			if (base.Random(0, 100) <= this.Chance)
			{
				Dictionary<string, string> dictionary = QudHistoryHelpers.BuildContextFromMemberTextFragments(this.entity, true);
				dictionary.Add("*sacredThing*", this.SacredThing.Coalesce(snapshotAtYear.sacredThing));
				dictionary.Add("*profaneThing*", this.ProfaneThing.Coalesce(snapshotAtYear.profaneThing));
				string text = Grammar.ConvertAtoAn(HistoricStringExpander.ExpandString("<spice." + this.SpiceElement + ".!random.capitalize>.", null, null, dictionary, null));
				base.SetEntityProperty("proverb", Grammar.InitCap(text));
				return;
			}
			base.SetEntityProperty("proverb", "Live and drink.");
		}

		// Token: 0x04002064 RID: 8292
		private string SpiceElement;

		// Token: 0x04002065 RID: 8293
		private string SacredThing;

		// Token: 0x04002066 RID: 8294
		private string ProfaneThing;

		// Token: 0x04002067 RID: 8295
		private int Chance;
	}
}
