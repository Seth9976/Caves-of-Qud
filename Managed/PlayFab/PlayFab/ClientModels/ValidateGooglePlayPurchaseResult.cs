using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200032B RID: 811
	[Serializable]
	public class ValidateGooglePlayPurchaseResult : PlayFabResultCommon
	{
		// Token: 0x04000FBD RID: 4029
		public List<PurchaseReceiptFulfillment> Fulfillments;
	}
}
