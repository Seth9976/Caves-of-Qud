using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;
using UnityEngine.Scripting;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001C RID: 28
	internal class GooglePurchaseUpdatedListener : AndroidJavaProxy, IGooglePurchaseUpdatedListener
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003010 File Offset: 0x00001210
		internal GooglePurchaseUpdatedListener(IGoogleLastKnownProductService googleLastKnownProductService, IGooglePurchaseCallback googlePurchaseCallback, IGooglePurchaseBuilder purchaseBuilder, IGoogleCachedQueryProductDetailsService googleCachedQueryProductDetailsService, IGooglePurchaseStateEnumProvider googlePurchaseStateEnumProvider, IGoogleQueryPurchasesService googleQueryPurchasesService = null)
			: base("com.android.billingclient.api.PurchasesUpdatedListener")
		{
			this.m_LastKnownProductService = googleLastKnownProductService;
			this.m_GooglePurchaseCallback = googlePurchaseCallback;
			this.m_GoogleCachedQueryProductDetailsService = googleCachedQueryProductDetailsService;
			this.m_GooglePurchaseStateEnumProvider = googlePurchaseStateEnumProvider;
			this.m_GoogleQueryPurchasesService = googleQueryPurchasesService;
			this.m_PurchaseBuilder = purchaseBuilder;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000304A File Offset: 0x0000124A
		public void SetGoogleQueryPurchaseService(IGoogleQueryPurchasesService googleFetchPurchases)
		{
			this.m_GoogleQueryPurchasesService = googleFetchPurchases;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003054 File Offset: 0x00001254
		[Preserve]
		public void onPurchasesUpdated(AndroidJavaObject billingResult, AndroidJavaObject javaPurchasesList)
		{
			List<AndroidJavaObject> list = javaPurchasesList.Enumerate().ToList<AndroidJavaObject>();
			IGoogleBillingResult googleBillingResult = new GoogleBillingResult(billingResult);
			List<IGooglePurchase> list2 = this.m_PurchaseBuilder.BuildPurchases(list).ToList<IGooglePurchase>();
			this.OnPurchasesUpdated(googleBillingResult, list2);
			foreach (AndroidJavaObject androidJavaObject in list)
			{
				if (androidJavaObject != null)
				{
					androidJavaObject.Dispose();
				}
			}
			billingResult.Dispose();
			if (javaPurchasesList != null)
			{
				javaPurchasesList.Dispose();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000030E4 File Offset: 0x000012E4
		internal void OnPurchasesUpdated(IGoogleBillingResult result, List<IGooglePurchase> purchases)
		{
			if (result.responseCode == GoogleBillingResponseCode.Ok)
			{
				this.HandleResultOkCases(result, purchases);
				return;
			}
			if (result.responseCode == GoogleBillingResponseCode.UserCanceled && purchases.Any<IGooglePurchase>())
			{
				this.ApplyOnPurchases(purchases, new Action<IGooglePurchase>(this.OnPurchaseCancelled));
				return;
			}
			if (result.responseCode == GoogleBillingResponseCode.ItemAlreadyOwned && purchases.Any<IGooglePurchase>())
			{
				this.ApplyOnPurchases(purchases, new Action<IGooglePurchase>(this.OnPurchaseAlreadyOwned));
				return;
			}
			this.HandleErrorCases(result, purchases);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003154 File Offset: 0x00001354
		private void HandleResultOkCases(IGoogleBillingResult result, List<IGooglePurchase> purchases)
		{
			if (purchases.Any<IGooglePurchase>())
			{
				this.ApplyOnPurchases(purchases, new Action<IGooglePurchase>(this.OnPurchaseOk));
				return;
			}
			this.HandleErrorCases(result, purchases);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000317A File Offset: 0x0000137A
		private void HandleErrorCases(IGoogleBillingResult billingResult, List<IGooglePurchase> purchases)
		{
			if (!purchases.Any<IGooglePurchase>())
			{
				this.HandleErrorGoogleBillingResult(billingResult);
				return;
			}
			this.ApplyOnPurchases(purchases, billingResult, new Action<IGooglePurchase, string>(this.OnPurchaseFailed));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031A0 File Offset: 0x000013A0
		private void HandleErrorGoogleBillingResult(IGoogleBillingResult billingResult)
		{
			GoogleBillingResponseCode responseCode = billingResult.responseCode;
			if (responseCode == GoogleBillingResponseCode.UserCanceled)
			{
				this.HandleUserCancelledPurchaseFailure(billingResult);
				return;
			}
			if (responseCode == GoogleBillingResponseCode.BillingUnavailable)
			{
				this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(this.m_LastKnownProductService.LastKnownProductId, PurchaseFailureReason.PurchasingUnavailable, billingResult.debugMessage + " - Google BillingResponseCode = " + billingResult.responseCode.ToString()));
				return;
			}
			if (responseCode == GoogleBillingResponseCode.ItemAlreadyOwned)
			{
				this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(this.m_LastKnownProductService.LastKnownProductId, PurchaseFailureReason.DuplicateTransaction, billingResult.debugMessage + " - Google BillingResponseCode = " + billingResult.responseCode.ToString()));
				return;
			}
			this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(this.m_LastKnownProductService.LastKnownProductId, PurchaseFailureReason.Unknown, billingResult.debugMessage + " {M: GPUL.HEC} - Google BillingResponseCode = " + billingResult.responseCode.ToString()));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003290 File Offset: 0x00001490
		private async void HandleUserCancelledPurchaseFailure(IGoogleBillingResult billingResult)
		{
			List<IGooglePurchase> list = await this.m_GoogleQueryPurchasesService.QueryPurchases();
			this.HandleUserCancelledPurchaseFailure(billingResult, list);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032D0 File Offset: 0x000014D0
		private void HandleUserCancelledPurchaseFailure(IGoogleBillingResult billingResult, List<IGooglePurchase> googlePurchases)
		{
			IGooglePurchase googlePurchase = googlePurchases.FirstOrDefault<IGooglePurchase>((IGooglePurchase purchase) => ((purchase != null) ? purchase.sku : null) == this.m_LastKnownProductService.LastKnownProductId);
			if (googlePurchase != null && !googlePurchase.IsAcknowledged())
			{
				this.OnPurchaseOk(googlePurchase);
				return;
			}
			this.OnPurchaseCancelled(billingResult);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000330C File Offset: 0x0000150C
		private void ApplyOnPurchases(List<IGooglePurchase> purchases, Action<IGooglePurchase> action)
		{
			foreach (IGooglePurchase googlePurchase in purchases)
			{
				action(googlePurchase);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000335C File Offset: 0x0000155C
		private void ApplyOnPurchases(IEnumerable<IGooglePurchase> purchases, IGoogleBillingResult billingResult, Action<IGooglePurchase, string> action)
		{
			foreach (IGooglePurchase googlePurchase in purchases)
			{
				action(googlePurchase, billingResult.debugMessage);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000033AC File Offset: 0x000015AC
		private void OnPurchaseOk(IGooglePurchase googlePurchase)
		{
			if (googlePurchase.purchaseState == this.m_GooglePurchaseStateEnumProvider.Purchased())
			{
				this.HandlePurchasedProduct(googlePurchase);
				return;
			}
			if (googlePurchase.purchaseState == this.m_GooglePurchaseStateEnumProvider.Pending())
			{
				this.m_GooglePurchaseCallback.NotifyDeferredPurchase(googlePurchase, googlePurchase.receipt, googlePurchase.purchaseToken);
				return;
			}
			this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(googlePurchase.purchaseToken, PurchaseFailureReason.Unknown, "PurchaseState is UNSPECIFIED_STATE, no more details exists {M: GPUL.OPO}"));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000341C File Offset: 0x0000161C
		private void HandlePurchasedProduct(IGooglePurchase googlePurchase)
		{
			if (this.IsDeferredSubscriptionChange(googlePurchase))
			{
				this.m_GooglePurchaseCallback.NotifyDeferredProrationUpgradeDowngradeSubscription(this.m_LastKnownProductService.LastKnownProductId);
				return;
			}
			this.m_GooglePurchaseCallback.OnPurchaseSuccessful(googlePurchase, googlePurchase.receipt, googlePurchase.purchaseToken);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003456 File Offset: 0x00001656
		private bool IsDeferredSubscriptionChange(IGooglePurchase googlePurchase)
		{
			return this.IsLastProrationModeDeferred() && googlePurchase.sku == this.m_LastKnownProductService.LastKnownOldProductId;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003478 File Offset: 0x00001678
		private bool IsLastProrationModeDeferred()
		{
			GooglePlayReplacementMode? lastKnownReplacementMode = this.m_LastKnownProductService.LastKnownReplacementMode;
			GooglePlayReplacementMode googlePlayReplacementMode = GooglePlayReplacementMode.Deferred;
			return (lastKnownReplacementMode.GetValueOrDefault() == googlePlayReplacementMode) & (lastKnownReplacementMode != null);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034A5 File Offset: 0x000016A5
		private void OnPurchaseCancelled(IGoogleBillingResult billingResult)
		{
			this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(this.m_LastKnownProductService.LastKnownProductId, PurchaseFailureReason.UserCancelled, billingResult.debugMessage));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034C9 File Offset: 0x000016C9
		private void OnPurchaseCancelled(IGooglePurchase googlePurchase)
		{
			this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(googlePurchase.purchaseToken, PurchaseFailureReason.UserCancelled, "User cancelled purchase"));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034E7 File Offset: 0x000016E7
		private void OnPurchaseAlreadyOwned(IGooglePurchase googlePurchase)
		{
			this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(googlePurchase.purchaseToken, PurchaseFailureReason.DuplicateTransaction, "Item already owned"));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003505 File Offset: 0x00001705
		private void OnPurchaseFailed(IGooglePurchase googlePurchase, string debugMessage)
		{
			this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(googlePurchase.purchaseToken, PurchaseFailureReason.Unknown, debugMessage + " {M: GPUL.OPF}"));
		}

		// Token: 0x04000033 RID: 51
		private const string k_AndroidPurchaseListenerClassName = "com.android.billingclient.api.PurchasesUpdatedListener";

		// Token: 0x04000034 RID: 52
		private readonly IGoogleLastKnownProductService m_LastKnownProductService;

		// Token: 0x04000035 RID: 53
		private readonly IGooglePurchaseCallback m_GooglePurchaseCallback;

		// Token: 0x04000036 RID: 54
		private readonly IGooglePurchaseBuilder m_PurchaseBuilder;

		// Token: 0x04000037 RID: 55
		private readonly IGoogleCachedQueryProductDetailsService m_GoogleCachedQueryProductDetailsService;

		// Token: 0x04000038 RID: 56
		private readonly IGooglePurchaseStateEnumProvider m_GooglePurchaseStateEnumProvider;

		// Token: 0x04000039 RID: 57
		private IGoogleQueryPurchasesService m_GoogleQueryPurchasesService;
	}
}
