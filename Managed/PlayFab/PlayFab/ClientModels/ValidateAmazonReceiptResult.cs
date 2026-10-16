using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000329 RID: 809
	[Serializable]
	public class ValidateAmazonReceiptResult : PlayFabResultCommon
	{
		// Token: 0x04000FB6 RID: 4022
		public List<PurchaseReceiptFulfillment> Fulfillments;
	}
}
