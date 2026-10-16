using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200032D RID: 813
	[Serializable]
	public class ValidateIOSReceiptResult : PlayFabResultCommon
	{
		// Token: 0x04000FC3 RID: 4035
		public List<PurchaseReceiptFulfillment> Fulfillments;
	}
}
