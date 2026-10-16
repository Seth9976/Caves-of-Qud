using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200000A RID: 10
	internal interface IAnalyticsClient
	{
		// Token: 0x06000028 RID: 40
		void OnPurchaseSucceeded(Product product);

		// Token: 0x06000029 RID: 41
		void OnPurchaseFailed(Product product, PurchaseFailureDescription purchaseFailureDescription);
	}
}
