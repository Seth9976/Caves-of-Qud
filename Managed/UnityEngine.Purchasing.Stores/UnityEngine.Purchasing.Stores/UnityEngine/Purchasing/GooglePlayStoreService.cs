using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Uniject;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;
using UnityEngine.Purchasing.Stores.Util;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000015 RID: 21
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GooglePlayStoreService : IGooglePlayStoreService
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000027C0 File Offset: 0x000009C0
		internal GooglePlayStoreService(IGoogleBillingClient billingClient, IQueryProductDetailsService queryProductDetailsService, IGooglePurchaseService purchaseService, IGoogleFinishTransactionService finishTransactionService, IGoogleQueryPurchasesService queryPurchasesService, IBillingClientStateListener billingClientStateListener, IGoogleLastKnownProductService lastKnownProductService, ITelemetryDiagnostics telemetryDiagnostics, ILogger logger, IRetryPolicy retryPolicy, IUtil util)
		{
			this.m_BillingClient = billingClient;
			this.m_QueryProductDetailsService = queryProductDetailsService;
			this.m_GooglePurchaseService = purchaseService;
			this.m_GoogleFinishTransactionService = finishTransactionService;
			this.m_GoogleQueryPurchasesService = queryPurchasesService;
			this.m_GoogleLastKnownProductService = lastKnownProductService;
			this.m_BillingClientStateListener = billingClientStateListener;
			this.m_TelemetryDiagnostics = telemetryDiagnostics;
			this.m_Logger = logger;
			this.m_RetryPolicy = retryPolicy;
			this.m_Util = util;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002845 File Offset: 0x00000A45
		internal void InitConnectionWithGooglePlay()
		{
			this.m_BillingClientStateListener.RegisterOnConnected(new Action(this.OnConnected));
			this.m_BillingClientStateListener.RegisterOnDisconnected(new Action<GoogleBillingResponseCode>(this.OnDisconnected));
			this.StartConnection();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000287B File Offset: 0x00000A7B
		private void StartConnection()
		{
			this.m_CurrentConnectionAttempts++;
			this.m_BillingClient.StartConnection(this.m_BillingClientStateListener);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000289C File Offset: 0x00000A9C
		public void ResumeConnection()
		{
			if (this.m_BillingClient.GetConnectionState() == GoogleBillingConnectionState.Disconnected)
			{
				this.AttemptReconnection();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028B1 File Offset: 0x00000AB1
		private void AttemptReconnection()
		{
			if (!this.AreConnectionAttemptsExhausted())
			{
				this.m_RetryPolicy.Invoke(delegate(Action retryAction)
				{
					this.RetryConnection(retryAction);
				}, null);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028D3 File Offset: 0x00000AD3
		private bool AreConnectionAttemptsExhausted()
		{
			return this.m_CurrentConnectionAttempts >= this.m_MaxConnectionAttempts;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000028E8 File Offset: 0x00000AE8
		private void RetryConnection(Action ActionToRetry)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				this.RetryConnectionAttempt(ActionToRetry);
			});
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002920 File Offset: 0x00000B20
		private void RetryConnectionAttempt(Action ActionToRetry)
		{
			if (!this.AreConnectionAttemptsExhausted() && this.m_BillingClient.GetConnectionState() == GoogleBillingConnectionState.Disconnected)
			{
				this.StartConnection();
				ActionToRetry();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002943 File Offset: 0x00000B43
		public bool IsConnectionReady()
		{
			return this.m_BillingClient.IsReady();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002950 File Offset: 0x00000B50
		private void OnConnected()
		{
			this.m_CurrentConnectionAttempts = 0;
			this.DequeueQueryProducts(GoogleBillingResponseCode.Ok);
			this.DequeueFetchPurchases();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002968 File Offset: 0x00000B68
		protected virtual void DequeueQueryProducts(GoogleBillingResponseCode googleBillingResponseCode)
		{
			ConcurrentQueue<ProductDescriptionQuery> concurrentQueue = new ConcurrentQueue<ProductDescriptionQuery>();
			bool flag = false;
			while (this.m_ProductsToQuery.Count > 0 && !flag)
			{
				GoogleBillingConnectionState connectionState = this.m_BillingClient.GetConnectionState();
				switch (connectionState)
				{
				case GoogleBillingConnectionState.Disconnected:
				{
					ProductDescriptionQuery productDescriptionQuery;
					if (this.m_ProductsToQuery.TryDequeue(out productDescriptionQuery) && productDescriptionQuery != null)
					{
						GoogleRetrieveProductsFailureReason googleRetrieveProductsFailureReason = (this.AreConnectionAttemptsExhausted() ? GoogleRetrieveProductsFailureReason.BillingServiceUnavailable : GoogleRetrieveProductsFailureReason.BillingServiceDisconnected);
						productDescriptionQuery.onRetrieveProductsFailed(googleRetrieveProductsFailureReason, googleBillingResponseCode);
						concurrentQueue.Enqueue(productDescriptionQuery);
					}
					break;
				}
				case GoogleBillingConnectionState.Connecting:
					flag = true;
					break;
				case GoogleBillingConnectionState.Connected:
				{
					ProductDescriptionQuery productDescriptionQuery2;
					if (this.m_ProductsToQuery.TryDequeue(out productDescriptionQuery2) && productDescriptionQuery2 != null)
					{
						this.m_QueryProductDetailsService.QueryAsyncProduct(productDescriptionQuery2.products, productDescriptionQuery2.onProductsReceived);
					}
					break;
				}
				default:
					this.m_Logger.LogIAPError(string.Format("GooglePlayStoreService state ({0}) unrecognized, cannot process ProductDescriptionQuery", connectionState));
					flag = true;
					break;
				}
			}
			foreach (ProductDescriptionQuery productDescriptionQuery3 in concurrentQueue)
			{
				this.m_ProductsToQuery.Enqueue(productDescriptionQuery3);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A84 File Offset: 0x00000C84
		protected virtual void DequeueFetchPurchases()
		{
			ConcurrentQueue<Action<List<IGooglePurchase>>> concurrentQueue = new ConcurrentQueue<Action<List<IGooglePurchase>>>();
			Action<List<IGooglePurchase>> action;
			while (this.m_OnPurchaseSucceededQueue.TryDequeue(out action))
			{
				concurrentQueue.Enqueue(action);
			}
			Action<List<IGooglePurchase>> action2;
			while (concurrentQueue.TryDequeue(out action2))
			{
				this.FetchPurchases(action2);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AC2 File Offset: 0x00000CC2
		private void OnDisconnected(GoogleBillingResponseCode googleBillingResponseCode)
		{
			this.DequeueQueryProducts(googleBillingResponseCode);
			this.AttemptReconnection();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002AD1 File Offset: 0x00000CD1
		public virtual void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products, Action<List<ProductDescription>, IGoogleBillingResult> onProductsReceived, Action<GoogleRetrieveProductsFailureReason, GoogleBillingResponseCode> onRetrieveProductsFailed)
		{
			if (this.m_BillingClient.GetConnectionState() == GoogleBillingConnectionState.Connected)
			{
				this.m_QueryProductDetailsService.QueryAsyncProduct(products, onProductsReceived);
				return;
			}
			this.HandleRetrieveProductsNotConnected(products, onProductsReceived, onRetrieveProductsFailed);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private void HandleRetrieveProductsNotConnected(ReadOnlyCollection<ProductDefinition> products, Action<List<ProductDescription>, IGoogleBillingResult> onProductsReceived, Action<GoogleRetrieveProductsFailureReason, GoogleBillingResponseCode> onRetrieveProductsFailed)
		{
			if (this.m_BillingClient.GetConnectionState() == GoogleBillingConnectionState.Disconnected)
			{
				if (this.AreConnectionAttemptsExhausted())
				{
					onRetrieveProductsFailed(GoogleRetrieveProductsFailureReason.BillingServiceUnavailable, GoogleBillingResponseCode.ServiceUnavailable);
				}
				else
				{
					onRetrieveProductsFailed(GoogleRetrieveProductsFailureReason.BillingServiceDisconnected, GoogleBillingResponseCode.ServiceDisconnected);
				}
			}
			this.m_ProductsToQuery.Enqueue(new ProductDescriptionQuery(products, onProductsReceived, onRetrieveProductsFailed));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B34 File Offset: 0x00000D34
		public void Purchase(ProductDefinition product)
		{
			this.Purchase(product, null, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B54 File Offset: 0x00000D54
		public virtual void Purchase(ProductDefinition product, [global::System.Runtime.CompilerServices.Nullable(2)] Product oldProduct, GooglePlayReplacementMode? desiredReplacementMode)
		{
			this.m_GoogleLastKnownProductService.LastKnownOldProductId = ((oldProduct != null) ? oldProduct.definition.storeSpecificId : null);
			this.m_GoogleLastKnownProductService.LastKnownProductId = product.storeSpecificId;
			this.m_GoogleLastKnownProductService.LastKnownReplacementMode = desiredReplacementMode;
			this.m_GooglePurchaseService.Purchase(product, oldProduct, desiredReplacementMode);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public void FinishTransaction(ProductDefinition product, string purchaseToken, Action<IGoogleBillingResult, IGooglePurchase> onTransactionFinished)
		{
			this.m_GoogleFinishTransactionService.FinishTransaction(product, purchaseToken, onTransactionFinished);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public async void FetchPurchases(Action<List<IGooglePurchase>> onQueryPurchaseSucceed)
		{
			try
			{
				await this.TryFetchPurchases(onQueryPurchaseSucceed);
			}
			catch (Exception ex)
			{
				this.m_TelemetryDiagnostics.SendDiagnostic("fetch_purchases_error", ex);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BF8 File Offset: 0x00000DF8
		private async Task TryFetchPurchases(Action<List<IGooglePurchase>> onQueryPurchaseSucceed)
		{
			if (onQueryPurchaseSucceed == null)
			{
				this.m_Logger.LogIAPWarning("FetchPurchases called with null callback onQueryPurchaseSucceed");
			}
			else if (this.m_BillingClient.GetConnectionState() == GoogleBillingConnectionState.Connected)
			{
				List<IGooglePurchase> list = await this.m_GoogleQueryPurchasesService.QueryPurchases();
				onQueryPurchaseSucceed(list);
			}
			else
			{
				this.m_OnPurchaseSucceededQueue.Enqueue(onQueryPurchaseSucceed);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C43 File Offset: 0x00000E43
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public IGooglePurchase GetPurchase(string purchaseToken, string skuType)
		{
			return this.m_GoogleQueryPurchasesService.GetPurchaseByToken(purchaseToken, skuType);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C52 File Offset: 0x00000E52
		public void SetMaxConnectionAttempts(int maxConnectionAttempts)
		{
			this.m_MaxConnectionAttempts = maxConnectionAttempts;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C5B File Offset: 0x00000E5B
		public void SetObfuscatedAccountId(string obfuscatedAccountId)
		{
			this.m_BillingClient.SetObfuscationAccountId(obfuscatedAccountId);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C69 File Offset: 0x00000E69
		public void SetObfuscatedProfileId(string obfuscatedProfileId)
		{
			this.m_BillingClient.SetObfuscationProfileId(obfuscatedProfileId);
		}

		// Token: 0x04000018 RID: 24
		private int m_CurrentConnectionAttempts;

		// Token: 0x04000019 RID: 25
		private int m_MaxConnectionAttempts = 3;

		// Token: 0x0400001A RID: 26
		private readonly IGoogleBillingClient m_BillingClient;

		// Token: 0x0400001B RID: 27
		private readonly IBillingClientStateListener m_BillingClientStateListener;

		// Token: 0x0400001C RID: 28
		private readonly IQueryProductDetailsService m_QueryProductDetailsService;

		// Token: 0x0400001D RID: 29
		private readonly ConcurrentQueue<ProductDescriptionQuery> m_ProductsToQuery = new ConcurrentQueue<ProductDescriptionQuery>();

		// Token: 0x0400001E RID: 30
		private readonly ConcurrentQueue<Action<List<IGooglePurchase>>> m_OnPurchaseSucceededQueue = new ConcurrentQueue<Action<List<IGooglePurchase>>>();

		// Token: 0x0400001F RID: 31
		private readonly IGooglePurchaseService m_GooglePurchaseService;

		// Token: 0x04000020 RID: 32
		private readonly IGoogleFinishTransactionService m_GoogleFinishTransactionService;

		// Token: 0x04000021 RID: 33
		private readonly IGoogleQueryPurchasesService m_GoogleQueryPurchasesService;

		// Token: 0x04000022 RID: 34
		private readonly IGoogleLastKnownProductService m_GoogleLastKnownProductService;

		// Token: 0x04000023 RID: 35
		private readonly ITelemetryDiagnostics m_TelemetryDiagnostics;

		// Token: 0x04000024 RID: 36
		private readonly ILogger m_Logger;

		// Token: 0x04000025 RID: 37
		private readonly IRetryPolicy m_RetryPolicy;

		// Token: 0x04000026 RID: 38
		private readonly IUtil m_Util;
	}
}
