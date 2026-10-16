using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200029C RID: 668
	[Serializable]
	public class PayForPurchaseResult : PlayFabResultCommon
	{
		// Token: 0x04000DE7 RID: 3559
		public uint CreditApplied;

		// Token: 0x04000DE8 RID: 3560
		public string OrderId;

		// Token: 0x04000DE9 RID: 3561
		public string ProviderData;

		// Token: 0x04000DEA RID: 3562
		public string ProviderToken;

		// Token: 0x04000DEB RID: 3563
		public string PurchaseConfirmationPageURL;

		// Token: 0x04000DEC RID: 3564
		public string PurchaseCurrency;

		// Token: 0x04000DED RID: 3565
		public uint PurchasePrice;

		// Token: 0x04000DEE RID: 3566
		public TransactionStatus? Status;

		// Token: 0x04000DEF RID: 3567
		public Dictionary<string, int> VCAmount;

		// Token: 0x04000DF0 RID: 3568
		public Dictionary<string, int> VirtualCurrency;
	}
}
