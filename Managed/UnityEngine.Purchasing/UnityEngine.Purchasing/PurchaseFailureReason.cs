using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000026 RID: 38
	public enum PurchaseFailureReason
	{
		// Token: 0x04000043 RID: 67
		PurchasingUnavailable,
		// Token: 0x04000044 RID: 68
		ExistingPurchasePending,
		// Token: 0x04000045 RID: 69
		ProductUnavailable,
		// Token: 0x04000046 RID: 70
		SignatureInvalid,
		// Token: 0x04000047 RID: 71
		UserCancelled,
		// Token: 0x04000048 RID: 72
		PaymentDeclined,
		// Token: 0x04000049 RID: 73
		DuplicateTransaction,
		// Token: 0x0400004A RID: 74
		Unknown
	}
}
