using System;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x0200019D RID: 413
	public enum SubscriptionProviderStatus
	{
		// Token: 0x040008FE RID: 2302
		NoError,
		// Token: 0x040008FF RID: 2303
		Cancelled,
		// Token: 0x04000900 RID: 2304
		UnknownError,
		// Token: 0x04000901 RID: 2305
		BillingError,
		// Token: 0x04000902 RID: 2306
		ProductUnavailable,
		// Token: 0x04000903 RID: 2307
		CustomerDidNotAcceptPriceChange,
		// Token: 0x04000904 RID: 2308
		FreeTrial,
		// Token: 0x04000905 RID: 2309
		PaymentPending
	}
}
