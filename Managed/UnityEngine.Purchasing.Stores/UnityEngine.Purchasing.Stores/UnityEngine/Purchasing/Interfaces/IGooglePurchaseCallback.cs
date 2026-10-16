using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x020000A2 RID: 162
	internal interface IGooglePurchaseCallback
	{
		// Token: 0x060003AD RID: 941
		void SetStoreCallback(IStoreCallback storeCallback);

		// Token: 0x060003AE RID: 942
		void SetStoreConfiguration(IGooglePlayConfigurationInternal configuration);

		// Token: 0x060003AF RID: 943
		void OnPurchaseSuccessful(IGooglePurchase purchase, string receipt, string purchaseToken);

		// Token: 0x060003B0 RID: 944
		void OnPurchaseFailed(PurchaseFailureDescription purchaseFailureDescription);

		// Token: 0x060003B1 RID: 945
		void NotifyDeferredPurchase(IGooglePurchase purchase, string receipt, string purchaseToken);

		// Token: 0x060003B2 RID: 946
		void NotifyDeferredProrationUpgradeDowngradeSubscription(string sku);
	}
}
