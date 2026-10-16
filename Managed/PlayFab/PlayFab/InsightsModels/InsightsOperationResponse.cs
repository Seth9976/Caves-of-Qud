using System;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x0200010B RID: 267
	[Serializable]
	public class InsightsOperationResponse : PlayFabResultCommon
	{
		// Token: 0x040005E8 RID: 1512
		public string Message;

		// Token: 0x040005E9 RID: 1513
		public string OperationId;

		// Token: 0x040005EA RID: 1514
		public string OperationType;
	}
}
