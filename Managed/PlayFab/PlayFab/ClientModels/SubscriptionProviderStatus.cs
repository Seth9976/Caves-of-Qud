using System;

namespace PlayFab.ClientModels
{
	// Token: 0x020002D7 RID: 727
	public enum SubscriptionProviderStatus
	{
		// Token: 0x04000ECC RID: 3788
		NoError,
		// Token: 0x04000ECD RID: 3789
		Cancelled,
		// Token: 0x04000ECE RID: 3790
		UnknownError,
		// Token: 0x04000ECF RID: 3791
		BillingError,
		// Token: 0x04000ED0 RID: 3792
		ProductUnavailable,
		// Token: 0x04000ED1 RID: 3793
		CustomerDidNotAcceptPriceChange,
		// Token: 0x04000ED2 RID: 3794
		FreeTrial,
		// Token: 0x04000ED3 RID: 3795
		PaymentPending
	}
}
