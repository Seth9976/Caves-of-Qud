using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x0200009E RID: 158
	internal interface IGooglePlayStoreService
	{
		// Token: 0x06000391 RID: 913
		void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products, Action<List<ProductDescription>, IGoogleBillingResult> onProductsReceived, Action<GoogleRetrieveProductsFailureReason, GoogleBillingResponseCode> onRetrieveProductFailed);

		// Token: 0x06000392 RID: 914
		void Purchase(ProductDefinition product);

		// Token: 0x06000393 RID: 915
		void Purchase(ProductDefinition product, Product oldProduct, GooglePlayReplacementMode? desiredReplacementMode);

		// Token: 0x06000394 RID: 916
		void FinishTransaction(ProductDefinition product, string purchaseToken, Action<IGoogleBillingResult, IGooglePurchase> onTransactionFinished);

		// Token: 0x06000395 RID: 917
		void FetchPurchases(Action<List<IGooglePurchase>> onQueryPurchaseSucceed);

		// Token: 0x06000396 RID: 918
		IGooglePurchase GetPurchase(string purchaseToken, string skuType);

		// Token: 0x06000397 RID: 919
		void SetMaxConnectionAttempts(int maxConnectionAttempts);

		// Token: 0x06000398 RID: 920
		void SetObfuscatedAccountId(string obfuscatedAccountId);

		// Token: 0x06000399 RID: 921
		void SetObfuscatedProfileId(string obfuscatedProfileId);

		// Token: 0x0600039A RID: 922
		void ResumeConnection();

		// Token: 0x0600039B RID: 923
		bool IsConnectionReady();
	}
}
