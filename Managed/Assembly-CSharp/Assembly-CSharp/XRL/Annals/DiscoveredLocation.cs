using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000606 RID: 1542
	[Serializable]
	public class DiscoveredLocation : HistoricEvent
	{
		// Token: 0x0600418B RID: 16779 RVA: 0x0014D91C File Offset: 0x0014BB1C
		public override void Generate()
		{
			this.duration = (long)base.Random(5, 8);
			HistoricEntity historicEntity = this.history.CreateEntity(this.year - (long)base.Random(0, 1000));
			historicEntity.ApplyEvent(new LocationConstructed(), long.MinValue);
			base.SetEventProperty("location", historicEntity.id);
			base.SetEventProperty("gospel", "discovered " + historicEntity.GetSnapshotAtYear(this.year).GetProperty("name", "unknown"));
		}
	}
}
