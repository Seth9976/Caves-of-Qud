using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	public class InsightsGetLimitsResponse : PlayFabResultCommon
	{
		// Token: 0x040005D6 RID: 1494
		public int DefaultPerformanceLevel;

		// Token: 0x040005D7 RID: 1495
		public int DefaultStorageRetentionDays;

		// Token: 0x040005D8 RID: 1496
		public int StorageMaxRetentionDays;

		// Token: 0x040005D9 RID: 1497
		public int StorageMinRetentionDays;

		// Token: 0x040005DA RID: 1498
		public List<InsightsPerformanceLevel> SubMeters;
	}
}
