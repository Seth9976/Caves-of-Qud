using System;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000B6 RID: 182
	internal enum GoogleBillingResponseCode
	{
		// Token: 0x04000218 RID: 536
		ServiceTimeout = -3,
		// Token: 0x04000219 RID: 537
		FeatureNotSupported,
		// Token: 0x0400021A RID: 538
		ServiceDisconnected,
		// Token: 0x0400021B RID: 539
		Ok,
		// Token: 0x0400021C RID: 540
		UserCanceled,
		// Token: 0x0400021D RID: 541
		ServiceUnavailable,
		// Token: 0x0400021E RID: 542
		BillingUnavailable,
		// Token: 0x0400021F RID: 543
		ItemUnavailable,
		// Token: 0x04000220 RID: 544
		DeveloperError,
		// Token: 0x04000221 RID: 545
		FatalError,
		// Token: 0x04000222 RID: 546
		ItemAlreadyOwned,
		// Token: 0x04000223 RID: 547
		ItemNotOwned,
		// Token: 0x04000224 RID: 548
		NetworkError = 12
	}
}
