using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002C RID: 44
	[Obsolete("Use GooglePlayReplacementMode instead")]
	public enum GooglePlayProrationMode
	{
		// Token: 0x04000069 RID: 105
		UnknownSubscriptionUpgradeDowngradePolicy,
		// Token: 0x0400006A RID: 106
		ImmediateWithTimeProration,
		// Token: 0x0400006B RID: 107
		ImmediateAndChargeProratedPrice,
		// Token: 0x0400006C RID: 108
		ImmediateWithoutProration,
		// Token: 0x0400006D RID: 109
		Deferred,
		// Token: 0x0400006E RID: 110
		ImmediateAndChargeFullPrice
	}
}
