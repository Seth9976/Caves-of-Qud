using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000612 RID: 1554
	[Serializable]
	public class ReshephCleansesGyre : HistoricEvent
	{
		// Token: 0x060041B4 RID: 16820 RVA: 0x001504A8 File Offset: 0x0014E6A8
		public override void Generate()
		{
			this.duration = 0L;
			string text = "In %" + this.year.ToString() + "%, Resheph cleansed the marshlands of the plagues of the Gyre and taught Abram to sow watervine along its fertile tracks.";
			base.SetEventProperty("gospel", text);
			base.SetEventProperty("JoppaShrine", "true");
		}
	}
}
