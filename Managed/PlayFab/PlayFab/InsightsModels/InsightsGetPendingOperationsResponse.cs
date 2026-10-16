using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	public class InsightsGetPendingOperationsResponse : PlayFabResultCommon
	{
		// Token: 0x040005E7 RID: 1511
		public List<InsightsGetOperationStatusResponse> PendingOperations;
	}
}
