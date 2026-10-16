using System;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000030 RID: 48
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IGooglePlayConfigurationInternal
	{
		// Token: 0x060000F7 RID: 247
		void NotifyInitializationConnectionFailed();

		// Token: 0x060000F8 RID: 248
		void NotifyDeferredPurchase([global::System.Runtime.CompilerServices.Nullable(2)] IStoreCallback storeCallback, IGooglePurchase purchase, string receipt, string transactionId);

		// Token: 0x060000F9 RID: 249
		void NotifyDeferredProrationUpgradeDowngradeSubscription([global::System.Runtime.CompilerServices.Nullable(2)] IStoreCallback storeCallback, string productId);

		// Token: 0x060000FA RID: 250
		bool IsFetchPurchasesAtInitializeSkipped();

		// Token: 0x060000FB RID: 251
		bool DoesRetrievePurchasesExcludeDeferred();

		// Token: 0x060000FC RID: 252
		void NotifyQueryProductDetailsFailed(int retryCount);
	}
}
