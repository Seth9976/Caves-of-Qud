using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200003C RID: 60
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GooglePlayStoreRetrieveProductsService : IGooglePlayStoreRetrieveProductsService
	{
		// Token: 0x0600013C RID: 316 RVA: 0x000049A9 File Offset: 0x00002BA9
		internal GooglePlayStoreRetrieveProductsService(IGooglePlayStoreService googlePlayStoreService, IGoogleFetchPurchases googleFetchPurchases, IGooglePlayConfigurationInternal googlePlayConfigurationInternal, IGooglePlayStoreExtensions googlePlayStoreExtensions)
		{
			this.m_GooglePlayStoreService = googlePlayStoreService;
			this.m_GoogleFetchPurchases = googleFetchPurchases;
			this.m_GooglePlayConfigurationInternal = googlePlayConfigurationInternal;
			this.m_GooglePlayStoreExtensions = googlePlayStoreExtensions;
			this.m_HasInitiallyRetrievedProducts = false;
			this.m_RetrieveProductsFailed = false;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000049DC File Offset: 0x00002BDC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public void SetStoreCallback(IStoreCallback storeCallback)
		{
			this.m_StoreCallback = storeCallback;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000049E8 File Offset: 0x00002BE8
		public void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products, bool wantPurchases = true)
		{
			if (wantPurchases)
			{
				this.m_GooglePlayStoreService.RetrieveProducts(products, new Action<List<ProductDescription>, IGoogleBillingResult>(this.OnProductsRetrievedWithPurchaseFetch), new Action<GoogleRetrieveProductsFailureReason, GoogleBillingResponseCode>(this.OnRetrieveProductsFailed));
				return;
			}
			this.m_GooglePlayStoreService.RetrieveProducts(products, new Action<List<ProductDescription>, IGoogleBillingResult>(this.OnProductsRetrieved), new Action<GoogleRetrieveProductsFailureReason, GoogleBillingResponseCode>(this.OnRetrieveProductsFailed));
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004A44 File Offset: 0x00002C44
		private void OnProductsRetrievedWithPurchaseFetch(List<ProductDescription> retrievedProducts, IGoogleBillingResult billingResult)
		{
			if (billingResult != null && retrievedProducts.Count == 0 && billingResult.responseCode != GoogleBillingResponseCode.Ok)
			{
				this.OnRetrieveProductsFailed(GoogleRetrieveProductsFailureReason.BillingServiceUnavailable, billingResult.responseCode);
				return;
			}
			this.m_HasInitiallyRetrievedProducts = true;
			this.m_GoogleFetchPurchases.FetchPurchases(delegate(List<Product> purchaseProducts)
			{
				List<ProductDescription> list = this.MakePurchasesIntoProducts(retrievedProducts, purchaseProducts);
				IStoreCallback storeCallback = this.m_StoreCallback;
				if (storeCallback == null)
				{
					return;
				}
				storeCallback.OnProductsRetrieved(list);
			});
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004AAC File Offset: 0x00002CAC
		private void OnProductsRetrieved(List<ProductDescription> retrievedProducts, IGoogleBillingResult billingResult)
		{
			if (billingResult != null && retrievedProducts.Count == 0 && billingResult.responseCode != GoogleBillingResponseCode.Ok)
			{
				this.OnRetrieveProductsFailed(GoogleRetrieveProductsFailureReason.BillingServiceUnavailable, billingResult.responseCode);
				return;
			}
			this.m_HasInitiallyRetrievedProducts = true;
			this.m_RetrieveProductsFailed = false;
			IStoreCallback storeCallback = this.m_StoreCallback;
			if (storeCallback == null)
			{
				return;
			}
			storeCallback.OnProductsRetrieved(retrievedProducts);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004AFC File Offset: 0x00002CFC
		private void OnRetrieveProductsFailed(GoogleRetrieveProductsFailureReason reason, GoogleBillingResponseCode responseCode)
		{
			if (reason == GoogleRetrieveProductsFailureReason.BillingServiceUnavailable && !this.m_HasInitiallyRetrievedProducts && !this.m_RetrieveProductsFailed)
			{
				this.m_RetrieveProductsFailed = true;
				this.m_GooglePlayConfigurationInternal.NotifyInitializationConnectionFailed();
				IStoreCallback storeCallback = this.m_StoreCallback;
				if (storeCallback == null)
				{
					return;
				}
				storeCallback.OnSetupFailed(InitializationFailureReason.PurchasingUnavailable, "GoogleBillingResponseCode: " + responseCode.ToString());
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004B57 File Offset: 0x00002D57
		public void ResumeConnection()
		{
			this.m_GooglePlayStoreService.ResumeConnection();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004B64 File Offset: 0x00002D64
		private List<ProductDescription> MakePurchasesIntoProducts(List<ProductDescription> retrievedProducts, IEnumerable<Product> purchaseProducts)
		{
			List<ProductDescription> list = new List<ProductDescription>(retrievedProducts);
			if (purchaseProducts != null)
			{
				using (IEnumerator<Product> enumerator = purchaseProducts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Product purchaseProduct = enumerator.Current;
						if (!this.m_GooglePlayConfigurationInternal.DoesRetrievePurchasesExcludeDeferred() || !this.IsPurchasedProductDeferred(purchaseProduct))
						{
							int num = list.FindLastIndex((ProductDescription product) => product.storeSpecificId == purchaseProduct.definition.storeSpecificId);
							if (num != -1)
							{
								ProductDescription productDescription = list[num];
								list[num] = new ProductDescription(productDescription.storeSpecificId, productDescription.metadata, purchaseProduct.receipt, purchaseProduct.transactionID, productDescription.type);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004C38 File Offset: 0x00002E38
		private bool IsPurchasedProductDeferred(Product product)
		{
			return this.m_GooglePlayStoreExtensions.IsPurchasedProductDeferred(product);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004C46 File Offset: 0x00002E46
		public bool HasInitiallyRetrievedProducts()
		{
			return this.m_HasInitiallyRetrievedProducts;
		}

		// Token: 0x0400008E RID: 142
		private readonly IGooglePlayStoreService m_GooglePlayStoreService;

		// Token: 0x0400008F RID: 143
		private readonly IGoogleFetchPurchases m_GoogleFetchPurchases;

		// Token: 0x04000090 RID: 144
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IStoreCallback m_StoreCallback;

		// Token: 0x04000091 RID: 145
		private readonly IGooglePlayConfigurationInternal m_GooglePlayConfigurationInternal;

		// Token: 0x04000092 RID: 146
		private readonly IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;

		// Token: 0x04000093 RID: 147
		private bool m_HasInitiallyRetrievedProducts;

		// Token: 0x04000094 RID: 148
		private bool m_RetrieveProductsFailed;
	}
}
