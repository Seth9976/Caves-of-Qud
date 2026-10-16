using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002CE RID: 718
	[Serializable]
	public class StartPurchaseResult : PlayFabResultCommon
	{
		// Token: 0x04000EAB RID: 3755
		public List<CartItem> Contents;

		// Token: 0x04000EAC RID: 3756
		public string OrderId;

		// Token: 0x04000EAD RID: 3757
		public List<PaymentOption> PaymentOptions;

		// Token: 0x04000EAE RID: 3758
		public Dictionary<string, int> VirtualCurrencyBalances;
	}
}
