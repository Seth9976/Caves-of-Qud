using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000613 RID: 1555
	[Serializable]
	public class ReshephClosesTomb : HistoricEvent
	{
		// Token: 0x060041B6 RID: 16822 RVA: 0x001504FC File Offset: 0x0014E6FC
		public override void Generate()
		{
			this.duration = 0L;
			string text = "In %" + this.year.ToString() + "%, Resheph closed the gates to the Tomb of the Eaters, abdicated the throne, and dissolved the sultanate in order to lift the curse of the Gyre.";
			base.SetEventProperty("gospel", text);
			base.SetEntityProperty("flipYear", this.year.ToString());
		}
	}
}
