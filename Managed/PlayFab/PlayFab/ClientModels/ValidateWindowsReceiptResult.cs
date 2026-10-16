using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200032F RID: 815
	[Serializable]
	public class ValidateWindowsReceiptResult : PlayFabResultCommon
	{
		// Token: 0x04000FC9 RID: 4041
		public List<PurchaseReceiptFulfillment> Fulfillments;
	}
}
