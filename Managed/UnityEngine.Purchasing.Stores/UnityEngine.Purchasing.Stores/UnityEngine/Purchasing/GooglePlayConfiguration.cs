using System;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000028 RID: 40
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GooglePlayConfiguration : IGooglePlayConfiguration, IStoreConfiguration, IGooglePlayConfigurationInternal
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00003D9B File Offset: 0x00001F9B
		public GooglePlayConfiguration(IGooglePlayStoreService googlePlayStoreService)
		{
			this.m_GooglePlayStoreService = googlePlayStoreService;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public void SetServiceDisconnectAtInitializeListener(Action action)
		{
			this.m_InitializationConnectionLister = action;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003DC1 File Offset: 0x00001FC1
		public void NotifyInitializationConnectionFailed()
		{
			Action initializationConnectionLister = this.m_InitializationConnectionLister;
			if (initializationConnectionLister == null)
			{
				return;
			}
			initializationConnectionLister();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003DD3 File Offset: 0x00001FD3
		public void SetQueryProductDetailsFailedListener(Action<int> action)
		{
			this.m_QueryProductDetailsFailedListener = action;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003DDC File Offset: 0x00001FDC
		public void NotifyQueryProductDetailsFailed(int retryCount)
		{
			Action<int> queryProductDetailsFailedListener = this.m_QueryProductDetailsFailedListener;
			if (queryProductDetailsFailedListener == null)
			{
				return;
			}
			queryProductDetailsFailedListener(retryCount);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003DEF File Offset: 0x00001FEF
		public void SetDeferredPurchaseListener(Action<Product> action)
		{
			this.m_DeferredPurchaseAction = action;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public void NotifyDeferredProrationUpgradeDowngradeSubscription([global::System.Runtime.CompilerServices.Nullable(2)] IStoreCallback storeCallback, string productId)
		{
			Product product = storeCallback.FindProductById(productId);
			if (product != null)
			{
				Action<Product> deferredProrationUpgradeDowngradeSubscriptionAction = this.m_DeferredProrationUpgradeDowngradeSubscriptionAction;
				if (deferredProrationUpgradeDowngradeSubscriptionAction == null)
				{
					return;
				}
				deferredProrationUpgradeDowngradeSubscriptionAction(product);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003E21 File Offset: 0x00002021
		public bool IsFetchPurchasesAtInitializeSkipped()
		{
			return !this.m_FetchPurchasesAtInitialize;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003E2C File Offset: 0x0000202C
		public bool DoesRetrievePurchasesExcludeDeferred()
		{
			return this.m_FetchPurchasesExcludeDeferred;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003E34 File Offset: 0x00002034
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public void NotifyDeferredPurchase(IStoreCallback storeCallback, IGooglePurchase purchase, string receipt, string transactionId)
		{
			Product product = ((storeCallback != null) ? storeCallback.FindProductById((purchase != null) ? purchase.sku : null) : null);
			if (product != null)
			{
				ProductPurchaseUpdater.UpdateProductReceiptAndTransactionID(product, receipt, transactionId, "GooglePlay");
				Action<Product> deferredPurchaseAction = this.m_DeferredPurchaseAction;
				if (deferredPurchaseAction == null)
				{
					return;
				}
				deferredPurchaseAction(product);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003E7C File Offset: 0x0000207C
		public void SetDeferredProrationUpgradeDowngradeSubscriptionListener(Action<Product> action)
		{
			this.m_DeferredProrationUpgradeDowngradeSubscriptionAction = action;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003E85 File Offset: 0x00002085
		public void SetObfuscatedAccountId(string accountId)
		{
			this.m_GooglePlayStoreService.SetObfuscatedAccountId(accountId);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003E93 File Offset: 0x00002093
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public void SetObfuscatedProfileId(string profileId)
		{
			this.m_GooglePlayStoreService.SetObfuscatedProfileId(profileId);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003EA1 File Offset: 0x000020A1
		public void SetFetchPurchasesAtInitialize(bool enable)
		{
			this.m_FetchPurchasesAtInitialize = enable;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003EAA File Offset: 0x000020AA
		public void SetFetchPurchasesExcludeDeferred(bool exclude)
		{
			this.m_FetchPurchasesExcludeDeferred = exclude;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003EB3 File Offset: 0x000020B3
		public void SetMaxConnectionAttempts(int maxConnectionAttempts)
		{
			this.m_GooglePlayStoreService.SetMaxConnectionAttempts(maxConnectionAttempts);
		}

		// Token: 0x04000056 RID: 86
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Action m_InitializationConnectionLister;

		// Token: 0x04000057 RID: 87
		private readonly IGooglePlayStoreService m_GooglePlayStoreService;

		// Token: 0x04000058 RID: 88
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private Action<Product> m_DeferredPurchaseAction;

		// Token: 0x04000059 RID: 89
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private Action<Product> m_DeferredProrationUpgradeDowngradeSubscriptionAction;

		// Token: 0x0400005A RID: 90
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Action<int> m_QueryProductDetailsFailedListener;

		// Token: 0x0400005B RID: 91
		private bool m_FetchPurchasesAtInitialize = true;

		// Token: 0x0400005C RID: 92
		private bool m_FetchPurchasesExcludeDeferred = true;
	}
}
