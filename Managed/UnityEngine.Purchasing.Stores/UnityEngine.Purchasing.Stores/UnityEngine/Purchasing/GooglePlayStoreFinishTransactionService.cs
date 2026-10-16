using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200003A RID: 58
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GooglePlayStoreFinishTransactionService : IGooglePlayStoreFinishTransactionService
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00004818 File Offset: 0x00002A18
		internal GooglePlayStoreFinishTransactionService(IGooglePlayStoreService googlePlayStoreService)
		{
			this.m_ProcessedPurchaseToken = new HashSet<string>();
			this.m_GooglePlayStoreService = googlePlayStoreService;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004832 File Offset: 0x00002A32
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public void SetStoreCallback(IStoreCallback storeCallback)
		{
			this.m_StoreCallback = storeCallback;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000483C File Offset: 0x00002A3C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public void FinishTransaction(ProductDefinition product, string purchaseToken)
		{
			this.m_GooglePlayStoreService.FinishTransaction(product, purchaseToken, delegate(IGoogleBillingResult billingResult, IGooglePurchase googlePurchase)
			{
				this.HandleFinishTransaction(product, billingResult, googlePurchase);
			});
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000487C File Offset: 0x00002A7C
		private void HandleFinishTransaction([global::System.Runtime.CompilerServices.Nullable(2)] ProductDefinition product, IGoogleBillingResult billingResult, IGooglePurchase purchase)
		{
			if (!this.m_ProcessedPurchaseToken.Contains(purchase.purchaseToken))
			{
				if (billingResult.responseCode == GoogleBillingResponseCode.Ok)
				{
					this.m_RetryCount = 0;
					this.m_ProcessedPurchaseToken.Add(purchase.purchaseToken);
					this.CallPurchaseSucceededUpdateReceipt(purchase);
					return;
				}
				if (this.m_RetryCount <= 5 && GooglePlayStoreFinishTransactionService.IsResponseCodeInRecoverableState(billingResult))
				{
					this.m_RetryCount++;
					this.FinishTransaction(product, purchase.purchaseToken);
					return;
				}
				IStoreCallback storeCallback = this.m_StoreCallback;
				if (storeCallback == null)
				{
					return;
				}
				storeCallback.OnPurchaseFailed(new PurchaseFailureDescription((product != null) ? product.storeSpecificId : null, PurchaseFailureReason.Unknown, billingResult.debugMessage + " {code: " + billingResult.responseCode.ToString() + ", M: GPSFTS.HFT}"));
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004940 File Offset: 0x00002B40
		private void CallPurchaseSucceededUpdateReceipt(IGooglePurchase googlePurchase)
		{
			IStoreCallback storeCallback = this.m_StoreCallback;
			if (storeCallback == null)
			{
				return;
			}
			storeCallback.OnPurchaseSucceeded(googlePurchase.sku ?? string.Empty, googlePurchase.receipt, googlePurchase.purchaseToken);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000496D File Offset: 0x00002B6D
		private static bool IsResponseCodeInRecoverableState(IGoogleBillingResult billingResult)
		{
			return billingResult.responseCode == GoogleBillingResponseCode.ServiceUnavailable || billingResult.responseCode == GoogleBillingResponseCode.DeveloperError || billingResult.responseCode == GoogleBillingResponseCode.FatalError;
		}

		// Token: 0x04000088 RID: 136
		private readonly HashSet<string> m_ProcessedPurchaseToken;

		// Token: 0x04000089 RID: 137
		private readonly IGooglePlayStoreService m_GooglePlayStoreService;

		// Token: 0x0400008A RID: 138
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IStoreCallback m_StoreCallback;

		// Token: 0x0400008B RID: 139
		private int m_RetryCount;

		// Token: 0x0400008C RID: 140
		private const int k_MaxRetryAttempts = 5;
	}
}
