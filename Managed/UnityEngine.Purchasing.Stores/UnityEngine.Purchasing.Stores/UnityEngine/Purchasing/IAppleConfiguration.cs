using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200005D RID: 93
	public interface IAppleConfiguration : IStoreConfiguration
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000216 RID: 534
		string appReceipt { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000217 RID: 535
		bool canMakePayments { get; }

		// Token: 0x06000218 RID: 536
		void SetApplePromotionalPurchaseInterceptorCallback(Action<Product> callback);

		// Token: 0x06000219 RID: 537
		void SetEntitlementsRevokedListener(Action<List<Product>> callback);
	}
}
