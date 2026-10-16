using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	public class InsightsGetDetailsResponse : PlayFabResultCommon
	{
		// Token: 0x040005D0 RID: 1488
		public uint DataUsageMb;

		// Token: 0x040005D1 RID: 1489
		public string ErrorMessage;

		// Token: 0x040005D2 RID: 1490
		public InsightsGetLimitsResponse Limits;

		// Token: 0x040005D3 RID: 1491
		public List<InsightsGetOperationStatusResponse> PendingOperations;

		// Token: 0x040005D4 RID: 1492
		public int PerformanceLevel;

		// Token: 0x040005D5 RID: 1493
		public int RetentionDays;
	}
}
