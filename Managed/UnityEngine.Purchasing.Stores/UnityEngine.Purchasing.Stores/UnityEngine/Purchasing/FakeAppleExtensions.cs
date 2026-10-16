using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200005B RID: 91
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class FakeAppleExtensions : IAppleExtensions, IStoreExtension
	{
		// Token: 0x06000203 RID: 515 RVA: 0x00007236 File Offset: 0x00005436
		public void RefreshAppReceipt(Action<string> successCallback, Action<string> errorCallback)
		{
			if (this.m_FailRefresh)
			{
				errorCallback("A fake error message");
			}
			else
			{
				successCallback("A fake refreshed receipt!");
			}
			this.m_FailRefresh = !this.m_FailRefresh;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007267 File Offset: 0x00005467
		[Obsolete("RefreshAppReceipt(Action<string> successCallback, Action errorCallback) is deprecated, please use RefreshAppReceipt(Action<string> successCallback, Action<string> errorCallback) instead.")]
		public void RefreshAppReceipt(Action<string> successCallback, Action errorCallback)
		{
			if (this.m_FailRefresh)
			{
				errorCallback();
			}
			else
			{
				successCallback("A fake refreshed receipt!");
			}
			this.m_FailRefresh = !this.m_FailRefresh;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007293 File Offset: 0x00005493
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[Obsolete("RestoreTransactions(Action<bool> callback) is deprecated, please use RestoreTransactions(Action<bool, string> callback) instead.")]
		public void RestoreTransactions(Action<bool> callback)
		{
			if (callback != null)
			{
				callback(true);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000729F File Offset: 0x0000549F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public void RestoreTransactions(Action<bool, string> callback)
		{
			if (callback != null)
			{
				callback(true, null);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000072AC File Offset: 0x000054AC
		public void RegisterPurchaseDeferredListener(Action<Product> callback)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000072AE File Offset: 0x000054AE
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000072B6 File Offset: 0x000054B6
		public bool simulateAskToBuy { get; set; }

		// Token: 0x0600020A RID: 522 RVA: 0x000072BF File Offset: 0x000054BF
		public void FetchStorePromotionOrder(Action<List<Product>> successCallback, Action errorCallback)
		{
			errorCallback();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000072C7 File Offset: 0x000054C7
		public void SetStorePromotionOrder(List<Product> products)
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000072C9 File Offset: 0x000054C9
		public void FetchStorePromotionVisibility(Product product, Action<string, AppleStorePromotionVisibility> successCallback, Action errorCallback)
		{
			errorCallback();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000072D1 File Offset: 0x000054D1
		public void SetStorePromotionVisibility(Product product, AppleStorePromotionVisibility visible)
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000072D3 File Offset: 0x000054D3
		public void SetApplicationUsername(string applicationUsername)
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000072D5 File Offset: 0x000054D5
		public string GetTransactionReceiptForProduct(Product product)
		{
			return "";
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000072DC File Offset: 0x000054DC
		public void ContinuePromotionalPurchases()
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000072DE File Offset: 0x000054DE
		public Dictionary<string, string> GetIntroductoryPriceDictionary()
		{
			return new Dictionary<string, string>();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000072E5 File Offset: 0x000054E5
		public Dictionary<string, string> GetProductDetails()
		{
			return new Dictionary<string, string>();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000072EC File Offset: 0x000054EC
		public void PresentCodeRedemptionSheet()
		{
		}

		// Token: 0x040000F3 RID: 243
		private bool m_FailRefresh;
	}
}
