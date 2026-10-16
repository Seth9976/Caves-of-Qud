using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000003 RID: 3
	internal interface INativeAppleStore : INativeStore
	{
		// Token: 0x06000003 RID: 3
		void SetUnityPurchasingCallback(UnityPurchasingCallback AsyncCallback);

		// Token: 0x06000004 RID: 4
		void RestoreTransactions();

		// Token: 0x06000005 RID: 5
		void RefreshAppReceipt();

		// Token: 0x06000006 RID: 6
		void AddTransactionObserver();

		// Token: 0x06000007 RID: 7
		void SetApplicationUsername(string applicationUsername);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8
		string appReceipt { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9
		double appReceiptModificationDate { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10
		bool canMakePayments { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		bool simulateAskToBuy { get; set; }

		// Token: 0x0600000D RID: 13
		void FetchStorePromotionOrder();

		// Token: 0x0600000E RID: 14
		void SetStorePromotionOrder(string json);

		// Token: 0x0600000F RID: 15
		void FetchStorePromotionVisibility(string productId);

		// Token: 0x06000010 RID: 16
		void SetStorePromotionVisibility(string productId, string visibility);

		// Token: 0x06000011 RID: 17
		string GetTransactionReceiptForProductId(string productId);

		// Token: 0x06000012 RID: 18
		void InterceptPromotionalPurchases();

		// Token: 0x06000013 RID: 19
		void ContinuePromotionalPurchases();

		// Token: 0x06000014 RID: 20
		void PresentCodeRedemptionSheet();
	}
}
