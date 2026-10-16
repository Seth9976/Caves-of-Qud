using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x0200060E RID: 1550
	[Serializable]
	public class ReshephAbsolvesRebekah : HistoricEvent
	{
		// Token: 0x060041AC RID: 16812 RVA: 0x00150310 File Offset: 0x0014E510
		public override void Generate()
		{
			this.duration = 0L;
			string text = "In %" + this.year.ToString() + "%, Rebekah died of glotrot, and on her deathbed Resheph forgave her and absolved her of sin. He allowed her to be buried in the village of Ezra, outside the gate to the Tomb of the Eaters and beneath the shadow of the Spindle.";
			base.SetEventProperty("gospel", text);
			base.SetEventProperty("rebekah", "true");
		}
	}
}
