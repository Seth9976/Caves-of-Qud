using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x0200061C RID: 1564
	[Serializable]
	public class ReshephRebuffsCurse : HistoricEvent
	{
		// Token: 0x060041C8 RID: 16840 RVA: 0x00150764 File Offset: 0x0014E964
		public override void Generate()
		{
			this.duration = 0L;
			string text = "In %" + this.year.ToString() + "%, the Gyre widened, and the final plague afflicted the land. The Girsh Nephilim rose from their cradles on the Moon Stair and slouched toward Qud to eat its young. Resheph rose to meet them in battle and cast them back.";
			base.SetEventProperty("gospel", text);
			base.SetEventProperty("gyreplagues", "true");
		}
	}
}
