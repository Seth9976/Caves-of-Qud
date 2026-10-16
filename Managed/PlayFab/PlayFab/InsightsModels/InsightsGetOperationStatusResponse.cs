using System;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x02000108 RID: 264
	[Serializable]
	public class InsightsGetOperationStatusResponse : PlayFabResultCommon
	{
		// Token: 0x040005DD RID: 1501
		public string Message;

		// Token: 0x040005DE RID: 1502
		public DateTime OperationCompletedTime;

		// Token: 0x040005DF RID: 1503
		public string OperationId;

		// Token: 0x040005E0 RID: 1504
		public DateTime OperationLastUpdated;

		// Token: 0x040005E1 RID: 1505
		public DateTime OperationStartedTime;

		// Token: 0x040005E2 RID: 1506
		public string OperationType;

		// Token: 0x040005E3 RID: 1507
		public int OperationValue;

		// Token: 0x040005E4 RID: 1508
		public string Status;
	}
}
