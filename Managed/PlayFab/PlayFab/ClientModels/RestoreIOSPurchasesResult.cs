using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002BF RID: 703
	[Serializable]
	public class RestoreIOSPurchasesResult : PlayFabResultCommon
	{
		// Token: 0x04000E76 RID: 3702
		public List<PurchaseReceiptFulfillment> Fulfillments;
	}
}
