using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Uniject;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;
using UnityEngine.Purchasing.Stores.Util;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000023 RID: 35
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class QueryProductDetailsService : IQueryProductDetailsService
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003A1C File Offset: 0x00001C1C
		internal QueryProductDetailsService(IGoogleBillingClient billingClient, IGoogleCachedQueryProductDetailsService googleCachedQueryProductDetailsService, IProductDetailsConverter productDetailsConverter, IRetryPolicy retryPolicy, IGoogleProductCallback googleProductCallback, IUtil util, ITelemetryDiagnostics telemetryDiagnostics)
		{
			this.m_BillingClient = billingClient;
			this.m_GoogleCachedQueryProductDetailsService = googleCachedQueryProductDetailsService;
			this.m_ProductDetailsConverter = productDetailsConverter;
			this.m_RetryPolicy = retryPolicy;
			this.m_GoogleProductCallback = googleProductCallback;
			this.m_Util = util;
			this.m_TelemetryDiagnostics = telemetryDiagnostics;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003A59 File Offset: 0x00001C59
		public void QueryAsyncProduct(ProductDefinition product, Action<List<AndroidJavaObject>, IGoogleBillingResult> onProductDetailsResponse)
		{
			this.QueryAsyncProduct(new List<ProductDefinition> { product }.AsReadOnly(), onProductDetailsResponse);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003A74 File Offset: 0x00001C74
		public void QueryAsyncProduct(ReadOnlyCollection<ProductDefinition> products, Action<List<ProductDescription>, IGoogleBillingResult> onProductDetailsResponse)
		{
			this.QueryAsyncProduct(products, delegate(List<AndroidJavaObject> productDetails, IGoogleBillingResult responseCode)
			{
				onProductDetailsResponse(this.m_ProductDetailsConverter.ConvertOnQueryProductDetailsResponse(productDetails), responseCode);
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public void QueryAsyncProduct(ReadOnlyCollection<ProductDefinition> products, Action<List<AndroidJavaObject>, IGoogleBillingResult> onProductDetailsResponse)
		{
			QueryProductDetailsService.<>c__DisplayClass10_0 CS$<>8__locals1 = new QueryProductDetailsService.<>c__DisplayClass10_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.products = products;
			CS$<>8__locals1.onProductDetailsResponse = onProductDetailsResponse;
			CS$<>8__locals1.retryCount = 0;
			this.m_RetryPolicy.Invoke(delegate(Action retryAction)
			{
				CS$<>8__locals1.<>4__this.QueryAsyncProductWithRetries(CS$<>8__locals1.products, CS$<>8__locals1.onProductDetailsResponse, retryAction);
			}, new Action(CS$<>8__locals1.<QueryAsyncProduct>g__OnActionRetry|1));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003AFC File Offset: 0x00001CFC
		private void QueryAsyncProductWithRetries(IReadOnlyCollection<ProductDefinition> products, Action<List<AndroidJavaObject>, IGoogleBillingResult> onProductDetailsResponse, Action retryQuery)
		{
			try
			{
				this.TryQueryAsyncProductWithRetries(products, onProductDetailsResponse, retryQuery);
			}
			catch (Exception ex)
			{
				this.m_TelemetryDiagnostics.SendDiagnostic("query_async_sku_error", ex);
				Debug.LogError(string.Format("Unity IAP - QueryAsyncProductWithRetries: {0}", ex));
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B48 File Offset: 0x00001D48
		private void TryQueryAsyncProductWithRetries(IReadOnlyCollection<ProductDefinition> products, Action<List<AndroidJavaObject>, IGoogleBillingResult> onProductDetailsResponse, Action retryQuery)
		{
			ProductDetailsResponseConsolidator productDetailsResponseConsolidator = new ProductDetailsResponseConsolidator(this.m_Util, this.m_TelemetryDiagnostics, delegate(IProductDetailsQueryResponse productDetailsQueryResponse)
			{
				this.m_GoogleCachedQueryProductDetailsService.AddCachedQueriedProductDetails(productDetailsQueryResponse.ProductDetails());
				if (this.ShouldRetryQuery(products, productDetailsQueryResponse))
				{
					retryQuery();
					return;
				}
				onProductDetailsResponse(this.GetCachedProductDetails(products).ToList<AndroidJavaObject>(), productDetailsQueryResponse.GetGoogleBillingResult());
			});
			this.QueryInAppsAsync(products, productDetailsResponseConsolidator);
			this.QuerySubsAsync(products, productDetailsResponseConsolidator);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003BAF File Offset: 0x00001DAF
		private bool ShouldRetryQuery(IEnumerable<ProductDefinition> requestedProducts, IProductDetailsQueryResponse queryResponse)
		{
			return !this.AreAllProductDetailsCached(requestedProducts) && queryResponse.IsRecoverable();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003BC2 File Offset: 0x00001DC2
		private bool AreAllProductDetailsCached(IEnumerable<ProductDefinition> products)
		{
			return products.Select<ProductDefinition, bool>(new Func<ProductDefinition, bool>(this.m_GoogleCachedQueryProductDetailsService.Contains)).All<bool>((bool isCached) => isCached);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003C00 File Offset: 0x00001E00
		private IEnumerable<AndroidJavaObject> GetCachedProductDetails(IEnumerable<ProductDefinition> products)
		{
			List<ProductDefinition> list = products.Where<ProductDefinition>(new Func<ProductDefinition, bool>(this.m_GoogleCachedQueryProductDetailsService.Contains)).ToList<ProductDefinition>();
			return this.m_GoogleCachedQueryProductDetailsService.GetCachedQueriedProductDetails(list);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003C38 File Offset: 0x00001E38
		private void QueryInAppsAsync(IEnumerable<ProductDefinition> products, IProductDetailsResponseConsolidator consolidator)
		{
			List<string> list = (from product in products
				where product.type != ProductType.Subscription
				select product.storeSpecificId).ToList<string>();
			this.QueryProductDetails(list, GoogleProductTypeEnum.InApp(), consolidator);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003CA4 File Offset: 0x00001EA4
		private void QuerySubsAsync(IEnumerable<ProductDefinition> products, IProductDetailsResponseConsolidator consolidator)
		{
			List<string> list = (from product in products
				where product.type == ProductType.Subscription
				select product.storeSpecificId).ToList<string>();
			this.QueryProductDetails(list, GoogleProductTypeEnum.Sub(), consolidator);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003D0D File Offset: 0x00001F0D
		private void QueryProductDetails(List<string> productList, string type, IProductDetailsResponseConsolidator consolidator)
		{
			if (productList.Count == 0)
			{
				consolidator.Consolidate(new GoogleBillingResult(null), new List<AndroidJavaObject>());
				return;
			}
			this.m_BillingClient.QueryProductDetailsAsync(productList, type, new Action<IGoogleBillingResult, List<AndroidJavaObject>>(consolidator.Consolidate));
		}

		// Token: 0x0400004E RID: 78
		private readonly IGoogleBillingClient m_BillingClient;

		// Token: 0x0400004F RID: 79
		private readonly IGoogleCachedQueryProductDetailsService m_GoogleCachedQueryProductDetailsService;

		// Token: 0x04000050 RID: 80
		private readonly IProductDetailsConverter m_ProductDetailsConverter;

		// Token: 0x04000051 RID: 81
		private readonly IRetryPolicy m_RetryPolicy;

		// Token: 0x04000052 RID: 82
		private readonly IGoogleProductCallback m_GoogleProductCallback;

		// Token: 0x04000053 RID: 83
		private readonly IUtil m_Util;

		// Token: 0x04000054 RID: 84
		private readonly ITelemetryDiagnostics m_TelemetryDiagnostics;
	}
}
