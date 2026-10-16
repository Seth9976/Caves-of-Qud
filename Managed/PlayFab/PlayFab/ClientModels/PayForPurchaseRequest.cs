using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200029B RID: 667
	[Serializable]
	public class PayForPurchaseRequest : PlayFabRequestCommon
	{
		// Token: 0x04000DE2 RID: 3554
		public string Currency;

		// Token: 0x04000DE3 RID: 3555
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000DE4 RID: 3556
		public string OrderId;

		// Token: 0x04000DE5 RID: 3557
		public string ProviderName;

		// Token: 0x04000DE6 RID: 3558
		public string ProviderTransactionId;
	}
}
