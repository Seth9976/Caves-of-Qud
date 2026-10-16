using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200029D RID: 669
	[Serializable]
	public class PaymentOption : PlayFabBaseModel
	{
		// Token: 0x04000DF1 RID: 3569
		public string Currency;

		// Token: 0x04000DF2 RID: 3570
		public uint Price;

		// Token: 0x04000DF3 RID: 3571
		public string ProviderName;

		// Token: 0x04000DF4 RID: 3572
		public uint StoreCredit;
	}
}
