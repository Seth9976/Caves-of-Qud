using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x0200060F RID: 1551
	[Serializable]
	public class ReshephAppointsRebekah : HistoricEvent
	{
		// Token: 0x060041AE RID: 16814 RVA: 0x00150364 File Offset: 0x0014E564
		public override void Generate()
		{
			this.duration = 0L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string newRegion = QudHistoryHelpers.GetNewRegion(this.history, snapshotAtYear.GetProperty("region", "unknown"));
			string text = string.Concat(new string[]
			{
				"In %",
				this.year.ToString(),
				"%, Resheph appointed Rebekah administrator of ",
				newRegion,
				". She tendered alms for the sick in his name."
			});
			base.SetEventProperty("gospel", text);
			base.SetEventProperty("rebekah", "true");
			base.SetEventProperty("rebekahWasHealer", "true");
			base.SetEventProperty("region", newRegion);
		}
	}
}
