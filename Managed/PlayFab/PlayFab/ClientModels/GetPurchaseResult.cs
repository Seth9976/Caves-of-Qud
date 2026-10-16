using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200023D RID: 573
	[Serializable]
	public class GetPurchaseResult : PlayFabResultCommon
	{
		// Token: 0x04000C77 RID: 3191
		public string OrderId;

		// Token: 0x04000C78 RID: 3192
		public string PaymentProvider;

		// Token: 0x04000C79 RID: 3193
		public DateTime PurchaseDate;

		// Token: 0x04000C7A RID: 3194
		public string TransactionId;

		// Token: 0x04000C7B RID: 3195
		public string TransactionStatus;
	}
}
