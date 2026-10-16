using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing.Extension
{
	// Token: 0x0200004A RID: 74
	public interface IStoreCallback
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000145 RID: 325
		ProductCollection products { get; }

		// Token: 0x06000146 RID: 326
		[Obsolete]
		void OnSetupFailed(InitializationFailureReason reason);

		// Token: 0x06000147 RID: 327
		void OnSetupFailed(InitializationFailureReason reason, string message);

		// Token: 0x06000148 RID: 328
		void OnProductsRetrieved(List<ProductDescription> products);

		// Token: 0x06000149 RID: 329
		void OnPurchaseSucceeded(string storeSpecificId, string receipt, string transactionIdentifier);

		// Token: 0x0600014A RID: 330
		void OnAllPurchasesRetrieved(List<Product> purchasedProducts);

		// Token: 0x0600014B RID: 331
		void OnPurchaseFailed(PurchaseFailureDescription desc);

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600014C RID: 332
		// (set) Token: 0x0600014D RID: 333
		bool useTransactionLog { get; set; }
	}
}
