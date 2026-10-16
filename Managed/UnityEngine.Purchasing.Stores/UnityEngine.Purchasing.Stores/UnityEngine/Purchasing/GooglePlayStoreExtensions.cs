using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;
using UnityEngine.Purchasing.Security;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002E RID: 46
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GooglePlayStoreExtensions : IGooglePlayStoreExtensions, IStoreExtension, IGooglePlayStoreExtensionsInternal
	{
		// Token: 0x060000DF RID: 223 RVA: 0x000040F3 File Offset: 0x000022F3
		internal GooglePlayStoreExtensions(IGooglePlayStoreService googlePlayStoreService, IGooglePurchaseStateEnumProvider googlePurchaseStateEnumProvider, ILogger logger, ITelemetryDiagnostics telemetryDiagnostics)
		{
			this.m_GooglePlayStoreService = googlePlayStoreService;
			this.m_GooglePurchaseStateEnumProvider = googlePurchaseStateEnumProvider;
			this.m_Logger = logger;
			this.m_TelemetryDiagnostics = telemetryDiagnostics;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004118 File Offset: 0x00002318
		public void UpgradeDowngradeSubscription(string oldSku, string newSku)
		{
			this.UpgradeDowngradeSubscription(oldSku, newSku, GooglePlayReplacementMode.WithoutProration);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004123 File Offset: 0x00002323
		public void UpgradeDowngradeSubscription(string oldSku, string newSku, int desiredProrationMode)
		{
			this.UpgradeDowngradeSubscription(oldSku, newSku, (GooglePlayReplacementMode)desiredProrationMode);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000412E File Offset: 0x0000232E
		public void UpgradeDowngradeSubscription(string oldSku, string newSku, GooglePlayProrationMode desiredProrationMode)
		{
			this.UpgradeDowngradeSubscription(oldSku, newSku, (GooglePlayReplacementMode)desiredProrationMode);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000413C File Offset: 0x0000233C
		public virtual void UpgradeDowngradeSubscription(string oldSku, string newSku, GooglePlayReplacementMode desiredReplacementMode)
		{
			Product product = this.m_StoreCallback.FindProductById(newSku);
			Product product2 = this.m_StoreCallback.FindProductById(oldSku);
			if (product == null || product.definition.type != ProductType.Subscription || product2 == null || product2.definition.type != ProductType.Subscription)
			{
				IStoreCallback storeCallback = this.m_StoreCallback;
				if (storeCallback == null)
				{
					return;
				}
				storeCallback.OnPurchaseFailed(new PurchaseFailureDescription(newSku ?? "", PurchaseFailureReason.ProductUnavailable, "Please verify that the products are subscriptions and are not null."));
				return;
			}
			else
			{
				if (!string.IsNullOrEmpty(product2.transactionID))
				{
					this.m_GooglePlayStoreService.Purchase(product.definition, product2, new GooglePlayReplacementMode?(desiredReplacementMode));
					return;
				}
				IStoreCallback storeCallback2 = this.m_StoreCallback;
				if (storeCallback2 == null)
				{
					return;
				}
				storeCallback2.OnPurchaseFailed(new PurchaseFailureDescription(newSku ?? "", PurchaseFailureReason.ProductUnavailable, "Invalid transaction id for old product: " + product2.definition.id));
				return;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004208 File Offset: 0x00002408
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[Obsolete("RestoreTransactions(Action<bool> callback) is deprecated, please use RestoreTransactions(Action<bool, string> callback) instead.")]
		public virtual void RestoreTransactions(Action<bool> callback)
		{
			if (callback == null)
			{
				this.m_Logger.LogIAPError("RestoreTransactions called with a null callback. Please provide a callback to avoid null pointer exceptions");
			}
			this.m_GooglePlayStoreService.FetchPurchases(delegate(List<IGooglePurchase> _)
			{
				Action<bool> callback2 = callback;
				if (callback2 == null)
				{
					return;
				}
				callback2(true);
			});
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004254 File Offset: 0x00002454
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public virtual void RestoreTransactions(Action<bool, string> callback)
		{
			if (callback == null)
			{
				this.m_Logger.LogIAPError("RestoreTransactions called with a null callback. Please provide a callback to avoid null pointer exceptions");
			}
			this.m_GooglePlayStoreService.FetchPurchases(delegate(List<IGooglePurchase> _)
			{
				Action<bool, string> callback2 = callback;
				if (callback2 == null)
				{
					return;
				}
				callback2(true, null);
			});
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000429D File Offset: 0x0000249D
		public void ConfirmSubscriptionPriceChange(string productId, Action<bool> callback)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000429F File Offset: 0x0000249F
		public void SetStoreCallback(IStoreCallback storeCallback)
		{
			this.m_StoreCallback = storeCallback;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000042A8 File Offset: 0x000024A8
		public bool IsPurchasedProductDeferred(Product product)
		{
			if (product == null)
			{
				this.m_Logger.LogIAPWarning("IsPurchasedProductDeferred: the product is null.");
				return false;
			}
			bool flag;
			try
			{
				flag = this.TryIsPurchasedProductDeferred(product);
			}
			catch (Exception ex)
			{
				this.m_TelemetryDiagnostics.SendDiagnostic("parse_receipt_transaction_error", ex);
				this.m_Logger.LogIAPWarning("Cannot parse Google receipt for transaction " + product.transactionID);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004318 File Offset: 0x00002518
		private bool TryIsPurchasedProductDeferred(Product product)
		{
			GooglePurchaseState purchaseState = this.GetPurchaseState(product);
			return purchaseState == GooglePurchaseState.Refunded || purchaseState == GooglePurchaseState.Deferred;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004338 File Offset: 0x00002538
		public GooglePurchaseState GetPurchaseState(Product product)
		{
			IGooglePurchase googlePurchase = this.GooglePurchaseFromProduct(product);
			if (googlePurchase == null || googlePurchase.purchaseState == 0)
			{
				throw new InvalidOperationException("Cannot find purchase for product: " + product.definition.id);
			}
			if (googlePurchase.purchaseState != this.m_GooglePurchaseStateEnumProvider.Purchased())
			{
				return GooglePurchaseState.Deferred;
			}
			return GooglePurchaseState.Purchased;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004389 File Offset: 0x00002589
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public string GetObfuscatedAccountId(Product product)
		{
			IGooglePurchase googlePurchase = this.GooglePurchaseFromProduct(product);
			if (googlePurchase == null)
			{
				return null;
			}
			return googlePurchase.obfuscatedAccountId;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000439D File Offset: 0x0000259D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public string GetObfuscatedProfileId(Product product)
		{
			IGooglePurchase googlePurchase = this.GooglePurchaseFromProduct(product);
			if (googlePurchase == null)
			{
				return null;
			}
			return googlePurchase.obfuscatedProfileId;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000043B4 File Offset: 0x000025B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private IGooglePurchase GooglePurchaseFromProduct(Product product)
		{
			string text = ((product.definition.type == ProductType.Subscription) ? GoogleProductTypeEnum.Sub() : GoogleProductTypeEnum.InApp());
			return this.m_GooglePlayStoreService.GetPurchase(product.transactionID, text);
		}

		// Token: 0x04000077 RID: 119
		private readonly IGooglePlayStoreService m_GooglePlayStoreService;

		// Token: 0x04000078 RID: 120
		private readonly IGooglePurchaseStateEnumProvider m_GooglePurchaseStateEnumProvider;

		// Token: 0x04000079 RID: 121
		private readonly ITelemetryDiagnostics m_TelemetryDiagnostics;

		// Token: 0x0400007A RID: 122
		private readonly ILogger m_Logger;

		// Token: 0x0400007B RID: 123
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IStoreCallback m_StoreCallback;

		// Token: 0x0400007C RID: 124
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private readonly Action<Product> m_DeferredPurchaseAction;

		// Token: 0x0400007D RID: 125
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private readonly Action<Product> m_DeferredProrationUpgradeDowngradeSubscriptionAction;
	}
}
