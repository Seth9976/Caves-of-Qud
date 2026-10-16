using System;

namespace HistoryKit
{
	// Token: 0x02000364 RID: 868
	[Serializable]
	public class CreatedHistoricEvent : HistoricEvent
	{
		// Token: 0x06002057 RID: 8279 RVA: 0x0009F02F File Offset: 0x0009D22F
		public override void Generate()
		{
			this.duration = 0L;
		}
	}
}
