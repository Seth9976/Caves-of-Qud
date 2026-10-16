using System;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x0200010C RID: 268
	[Serializable]
	public class InsightsPerformanceLevel : PlayFabBaseModel
	{
		// Token: 0x040005EB RID: 1515
		public int ActiveEventExports;

		// Token: 0x040005EC RID: 1516
		public int CacheSizeMB;

		// Token: 0x040005ED RID: 1517
		public int Concurrency;

		// Token: 0x040005EE RID: 1518
		public double CreditsPerMinute;

		// Token: 0x040005EF RID: 1519
		public int EventsPerSecond;

		// Token: 0x040005F0 RID: 1520
		public int Level;

		// Token: 0x040005F1 RID: 1521
		public int MaxMemoryPerQueryMB;

		// Token: 0x040005F2 RID: 1522
		public int VirtualCpuCores;
	}
}
