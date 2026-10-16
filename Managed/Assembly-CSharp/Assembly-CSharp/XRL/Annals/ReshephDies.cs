using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000614 RID: 1556
	[Serializable]
	public class ReshephDies : HistoricEvent
	{
		// Token: 0x060041B8 RID: 16824 RVA: 0x00150558 File Offset: 0x0014E758
		public override void Generate()
		{
			this.duration = 0L;
			string text = "In %" + this.year.ToString() + "%, Resheph, the Above, Ghost-in-Cerulean, the Coiled Lamb of Baetyls, died of natural causes. He was 216 years old.";
			base.SetEventProperty("gospel", text);
		}
	}
}
