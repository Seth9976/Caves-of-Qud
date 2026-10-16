using System;
using System.Collections.ObjectModel;
using Uniject;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002D RID: 45
	internal class GooglePlayStore : AbstractStore
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00003FBC File Offset: 0x000021BC
		public GooglePlayStore(IGooglePlayStoreRetrieveProductsService retrieveProductsService, IGooglePlayStorePurchaseService storePurchaseService, IGoogleFetchPurchases fetchPurchases, IGooglePlayStoreFinishTransactionService transactionService, IGooglePurchaseCallback googlePurchaseCallback, IGooglePlayConfigurationInternal googlePlayConfigurationInternal, IGooglePlayStoreExtensionsInternal googlePlayStoreExtensions, IUtil util)
		{
			this.m_Util = util;
			this.m_RetrieveProductsService = retrieveProductsService;
			this.m_StorePurchaseService = storePurchaseService;
			this.m_FetchPurchases = fetchPurchases;
			this.m_FinishTransactionService = transactionService;
			this.m_GooglePurchaseCallback = googlePurchaseCallback;
			this.m_GooglePlayConfigurationInternal = googlePlayConfigurationInternal;
			this.m_GooglePlayStoreExtensions = googlePlayStoreExtensions;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000400C File Offset: 0x0000220C
		public override void Initialize(IStoreCallback callback)
		{
			ScriptingStoreCallback scriptingStoreCallback = new ScriptingStoreCallback(callback, this.m_Util);
			this.m_RetrieveProductsService.SetStoreCallback(scriptingStoreCallback);
			this.m_FetchPurchases.SetStoreCallback(scriptingStoreCallback);
			this.m_FinishTransactionService.SetStoreCallback(scriptingStoreCallback);
			this.m_GooglePurchaseCallback.SetStoreCallback(scriptingStoreCallback);
			this.m_GooglePlayStoreExtensions.SetStoreCallback(scriptingStoreCallback);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004064 File Offset: 0x00002264
		public override void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products)
		{
			bool flag = this.ShouldFetchPurchasesNext();
			this.m_RetrieveProductsService.RetrieveProducts(products, flag);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004085 File Offset: 0x00002285
		private bool HasInitiallyRetrievedProducts()
		{
			return this.m_RetrieveProductsService.HasInitiallyRetrievedProducts();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004094 File Offset: 0x00002294
		private bool ShouldFetchPurchasesNext()
		{
			bool flag = true;
			if (!this.HasInitiallyRetrievedProducts())
			{
				flag = !this.m_GooglePlayConfigurationInternal.IsFetchPurchasesAtInitializeSkipped();
			}
			return flag;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000040BB File Offset: 0x000022BB
		public override void Purchase(ProductDefinition product, string dummy)
		{
			this.m_StorePurchaseService.Purchase(product);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000040C9 File Offset: 0x000022C9
		public override void FinishTransaction(ProductDefinition product, string transactionId)
		{
			this.m_FinishTransactionService.FinishTransaction(product, transactionId);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000040D8 File Offset: 0x000022D8
		public void OnPause(bool isPaused)
		{
			if (!isPaused)
			{
				this.m_RetrieveProductsService.ResumeConnection();
				this.m_FetchPurchases.FetchPurchases();
			}
		}

		// Token: 0x0400006F RID: 111
		private readonly IGooglePlayStoreRetrieveProductsService m_RetrieveProductsService;

		// Token: 0x04000070 RID: 112
		private readonly IGooglePlayStorePurchaseService m_StorePurchaseService;

		// Token: 0x04000071 RID: 113
		private readonly IGoogleFetchPurchases m_FetchPurchases;

		// Token: 0x04000072 RID: 114
		private readonly IGooglePlayStoreFinishTransactionService m_FinishTransactionService;

		// Token: 0x04000073 RID: 115
		private readonly IGooglePurchaseCallback m_GooglePurchaseCallback;

		// Token: 0x04000074 RID: 116
		private readonly IGooglePlayStoreExtensionsInternal m_GooglePlayStoreExtensions;

		// Token: 0x04000075 RID: 117
		private readonly IGooglePlayConfigurationInternal m_GooglePlayConfigurationInternal;

		// Token: 0x04000076 RID: 118
		private readonly IUtil m_Util;
	}
}
