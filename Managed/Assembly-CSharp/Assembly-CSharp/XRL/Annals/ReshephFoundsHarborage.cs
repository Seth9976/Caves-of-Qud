using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000615 RID: 1557
	[Serializable]
	public class ReshephFoundsHarborage : HistoricEvent
	{
		// Token: 0x060041BA RID: 16826 RVA: 0x0015059C File Offset: 0x0014E79C
		public override void Generate()
		{
			this.duration = 0L;
			string text = "After conferring with mysterious strangers, Resheph convinced them to help him found a harborage for the purpose of receiving visitors from across the clustered cosmos. They named it Starfarer's Quay.";
			base.SetEventProperty("gospel", text);
		}
	}
}
