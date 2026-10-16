using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000611 RID: 1553
	[Serializable]
	public class ReshephBetrayed : HistoricEvent
	{
		// Token: 0x060041B2 RID: 16818 RVA: 0x00150464 File Offset: 0x0014E664
		public override void Generate()
		{
			this.duration = 0L;
			string text = "In %" + this.year.ToString() + "%, Rebekah betrayed Resheph by stealing the Mark of Death from the gate to the Tomb of the Eaters. In punishment, Resheph excommunicated her from the sultanate.";
			base.SetEventProperty("gospel", text);
		}
	}
}
