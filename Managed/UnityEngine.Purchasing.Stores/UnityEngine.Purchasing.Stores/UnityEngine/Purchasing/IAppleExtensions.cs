using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200005E RID: 94
	public interface IAppleExtensions : IStoreExtension
	{
		// Token: 0x0600021A RID: 538
		void RefreshAppReceipt(Action<string> successCallback, Action<string> errorCallback);

		// Token: 0x0600021B RID: 539
		[Obsolete("RefreshAppReceipt(Action<string> successCallback, Action errorCallback) is deprecated, please use RefreshAppReceipt(Action<string> successCallback, Action<string> errorCallback) instead.")]
		void RefreshAppReceipt(Action<string> successCallback, Action errorCallback);

		// Token: 0x0600021C RID: 540
		string GetTransactionReceiptForProduct(Product product);

		// Token: 0x0600021D RID: 541
		[Obsolete("RestoreTransactions(Action<bool> callback) is deprecated, please use RestoreTransactions(Action<bool, string> callback) instead.")]
		void RestoreTransactions(Action<bool> callback);

		// Token: 0x0600021E RID: 542
		void RestoreTransactions(Action<bool, string> callback);

		// Token: 0x0600021F RID: 543
		void RegisterPurchaseDeferredListener(Action<Product> callback);

		// Token: 0x06000220 RID: 544
		void SetApplicationUsername(string applicationUsername);

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000221 RID: 545
		// (set) Token: 0x06000222 RID: 546
		bool simulateAskToBuy { get; set; }

		// Token: 0x06000223 RID: 547
		void FetchStorePromotionOrder(Action<List<Product>> successCallback, Action errorCallback);

		// Token: 0x06000224 RID: 548
		void SetStorePromotionOrder(List<Product> products);

		// Token: 0x06000225 RID: 549
		void FetchStorePromotionVisibility(Product product, Action<string, AppleStorePromotionVisibility> successCallback, Action errorCallback);

		// Token: 0x06000226 RID: 550
		void SetStorePromotionVisibility(Product product, AppleStorePromotionVisibility visible);

		// Token: 0x06000227 RID: 551
		void ContinuePromotionalPurchases();

		// Token: 0x06000228 RID: 552
		Dictionary<string, string> GetIntroductoryPriceDictionary();

		// Token: 0x06000229 RID: 553
		Dictionary<string, string> GetProductDetails();

		// Token: 0x0600022A RID: 554
		void PresentCodeRedemptionSheet();
	}
}
