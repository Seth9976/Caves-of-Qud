using System;
using System.Collections.Generic;
using System.Linq;
using Uniject;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000039 RID: 57
	internal class GoogleFetchPurchases : IGoogleFetchPurchases
	{
		// Token: 0x06000127 RID: 295 RVA: 0x0000455F File Offset: 0x0000275F
		internal GoogleFetchPurchases(IGooglePlayStoreService googlePlayStoreService, IUtil util)
		{
			this.m_GooglePlayStoreService = googlePlayStoreService;
			this.m_Util = util;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004575 File Offset: 0x00002775
		public void SetStoreCallback(IStoreCallback storeCallback)
		{
			this.m_StoreCallback = storeCallback;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000457E File Offset: 0x0000277E
		public void FetchPurchases()
		{
			this.m_GooglePlayStoreService.FetchPurchases(new Action<List<IGooglePurchase>>(this.OnFetchedPurchase));
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004598 File Offset: 0x00002798
		public void FetchPurchases(Action<List<Product>> onQueryPurchaseSucceed)
		{
			this.m_GooglePlayStoreService.FetchPurchases(delegate(List<IGooglePurchase> googlePurchases)
			{
				onQueryPurchaseSucceed(this.FillProductsWithPurchases(googlePurchases));
			});
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000045D0 File Offset: 0x000027D0
		private List<Product> FillProductsWithPurchases(IEnumerable<IGooglePurchase> purchases)
		{
			return purchases.SelectMany<IGooglePurchase, Product>(new Func<IGooglePurchase, IEnumerable<Product>>(this.BuildProductsFromPurchase)).ToList<Product>();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000045EC File Offset: 0x000027EC
		private IEnumerable<Product> BuildProductsFromPurchase(IGooglePurchase purchase)
		{
			IGooglePurchase purchase2 = purchase;
			object obj;
			if (purchase2 == null)
			{
				obj = null;
			}
			else
			{
				List<string> skus = purchase2.skus;
				obj = ((skus != null) ? skus.Select<string, Product>(delegate(string sku)
				{
					IStoreCallback storeCallback = this.m_StoreCallback;
					if (storeCallback == null)
					{
						return null;
					}
					return storeCallback.FindProductById(sku);
				}).NonNull<Product>() : null);
			}
			object obj2 = obj;
			if (obj2 == null)
			{
				return null;
			}
			return obj2.Select<Product, Product>((Product product) => GoogleFetchPurchases.CompleteProductInfoWithPurchase(product, purchase));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004653 File Offset: 0x00002853
		private static Product CompleteProductInfoWithPurchase(Product product, IGooglePurchase purchase)
		{
			return new Product(product.definition, product.metadata, purchase.receipt)
			{
				transactionID = purchase.purchaseToken
			};
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004678 File Offset: 0x00002878
		private void OnFetchedPurchase(List<IGooglePurchase> purchases)
		{
			List<IGooglePurchase> list = purchases.Where<IGooglePurchase>(GoogleFetchPurchases.PurchaseIsPurchased()).ToList<IGooglePurchase>();
			List<Product> list2 = this.FillProductsWithPurchases(list);
			if (!list2.Any<Product>())
			{
				return;
			}
			IStoreCallback storeCallback = this.m_StoreCallback;
			if (storeCallback != null)
			{
				storeCallback.OnAllPurchasesRetrieved(list2);
			}
			List<IGooglePurchase> deferredPurchases = purchases.Where<IGooglePurchase>(GoogleFetchPurchases.PurchaseIsPending()).ToList<IGooglePurchase>();
			this.m_Util.RunOnMainThread(delegate
			{
				this.UpdateDeferredProductsByPurchases(deferredPurchases);
			});
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000046F3 File Offset: 0x000028F3
		private static Func<IGooglePurchase, bool> PurchaseIsPurchased()
		{
			return (IGooglePurchase purchase) => purchase.IsPurchased();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004714 File Offset: 0x00002914
		private static Func<IGooglePurchase, bool> PurchaseIsPending()
		{
			return (IGooglePurchase purchase) => purchase.IsPending();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004738 File Offset: 0x00002938
		private void UpdateDeferredProductsByPurchases(List<IGooglePurchase> deferredPurchases)
		{
			foreach (IGooglePurchase googlePurchase in deferredPurchases)
			{
				this.UpdateDeferredProductsByPurchase(googlePurchase);
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004788 File Offset: 0x00002988
		private void UpdateDeferredProductsByPurchase(IGooglePurchase deferredPurchase)
		{
			foreach (string text in deferredPurchase.skus)
			{
				this.UpdateDeferredProduct(deferredPurchase, text);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000047DC File Offset: 0x000029DC
		private void UpdateDeferredProduct(IGooglePurchase deferredPurchase, string sku)
		{
			IStoreCallback storeCallback = this.m_StoreCallback;
			Product product = ((storeCallback != null) ? storeCallback.FindProductById(sku) : null);
			if (product != null)
			{
				product.receipt = deferredPurchase.receipt;
				product.transactionID = deferredPurchase.purchaseToken;
			}
		}

		// Token: 0x04000085 RID: 133
		private readonly IGooglePlayStoreService m_GooglePlayStoreService;

		// Token: 0x04000086 RID: 134
		private IStoreCallback m_StoreCallback;

		// Token: 0x04000087 RID: 135
		private IUtil m_Util;
	}
}
