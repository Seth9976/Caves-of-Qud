using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000016 RID: 22
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GooglePurchaseService : IGooglePurchaseService
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002C80 File Offset: 0x00000E80
		internal GooglePurchaseService(IGoogleBillingClient billingClient, IGooglePurchaseCallback googlePurchaseCallback, IQueryProductDetailsService queryProductDetailsService)
		{
			this.m_BillingClient = billingClient;
			this.m_GooglePurchaseCallback = googlePurchaseCallback;
			this.m_QueryProductDetailsService = queryProductDetailsService;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public void Purchase(ProductDefinition product, [global::System.Runtime.CompilerServices.Nullable(2)] Product oldProduct, GooglePlayReplacementMode? desiredProrationMode)
		{
			this.m_QueryProductDetailsService.QueryAsyncProduct(product, delegate(List<AndroidJavaObject> productDetailsList, IGoogleBillingResult _)
			{
				this.OnQueryProductDetailsResponse(productDetailsList, product, oldProduct, desiredProrationMode);
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CEC File Offset: 0x00000EEC
		private void OnQueryProductDetailsResponse(List<AndroidJavaObject> productDetailsList, ProductDefinition productToBuy, [global::System.Runtime.CompilerServices.Nullable(2)] Product oldProduct, GooglePlayReplacementMode? desiredProrationMode)
		{
			if (this.ValidateQueryProductDetailsResponseParams(productDetailsList, productToBuy, oldProduct))
			{
				this.LaunchGoogleBillingFlow(productDetailsList[0], oldProduct, desiredProrationMode);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D09 File Offset: 0x00000F09
		private bool ValidateQueryProductDetailsResponseParams(List<AndroidJavaObject> skus, ProductDefinition productToBuy, [global::System.Runtime.CompilerServices.Nullable(2)] Product oldProduct)
		{
			if (!this.ValidateSkus(skus))
			{
				this.PurchaseFailedSkuNotFound(productToBuy);
				return false;
			}
			if (!this.ValidateOldProduct(oldProduct))
			{
				this.PurchaseFailedInvalidOldProduct(productToBuy, oldProduct);
				return false;
			}
			return true;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D31 File Offset: 0x00000F31
		private bool ValidateSkus([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] List<AndroidJavaObject> skus)
		{
			GooglePurchaseService.VerifyAndWarnIfMoreThanOneSku(skus);
			return skus != null && skus.Count > 0;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D47 File Offset: 0x00000F47
		private static void VerifyAndWarnIfMoreThanOneSku([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] List<AndroidJavaObject> skus)
		{
			if (skus != null && skus.Count > 1)
			{
				Debug.LogWarning(GoogleBillingStrings.getWarningMessageMoreThanOneSkuFound(skus[0].Call<string>("getProductId", Array.Empty<object>())));
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D75 File Offset: 0x00000F75
		private void PurchaseFailedSkuNotFound(ProductDefinition productToBuy)
		{
			this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(productToBuy.id, PurchaseFailureReason.ProductUnavailable, "SKU does not exist in the store."));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D93 File Offset: 0x00000F93
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool ValidateOldProduct(Product oldProduct)
		{
			return ((oldProduct != null) ? oldProduct.transactionID : null) != "";
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002DAB File Offset: 0x00000FAB
		private void PurchaseFailedInvalidOldProduct(ProductDefinition productToBuy, [global::System.Runtime.CompilerServices.Nullable(2)] Product oldProduct)
		{
			this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(productToBuy.id, PurchaseFailureReason.ProductUnavailable, "Invalid transaction id for old product: " + ((oldProduct != null) ? oldProduct.definition.id : null)));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DE0 File Offset: 0x00000FE0
		private void LaunchGoogleBillingFlow(AndroidJavaObject productToPurchase, [global::System.Runtime.CompilerServices.Nullable(2)] Product oldProduct, GooglePlayReplacementMode? desiredProrationMode)
		{
			AndroidJavaObject androidJavaObject = this.m_BillingClient.LaunchBillingFlow(productToPurchase, (oldProduct != null) ? oldProduct.transactionID : null, desiredProrationMode);
			this.HandleBillingFlowResult(new GoogleBillingResult(androidJavaObject), productToPurchase);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E14 File Offset: 0x00001014
		private void HandleBillingFlowResult(IGoogleBillingResult billingResult, AndroidJavaObject sku)
		{
			if (billingResult.responseCode != GoogleBillingResponseCode.Ok)
			{
				this.m_GooglePurchaseCallback.OnPurchaseFailed(new PurchaseFailureDescription(sku.Call<string>("getProductId", Array.Empty<object>()), PurchaseFailureReason.PurchasingUnavailable, billingResult.debugMessage));
			}
		}

		// Token: 0x04000027 RID: 39
		private readonly IGoogleBillingClient m_BillingClient;

		// Token: 0x04000028 RID: 40
		private readonly IGooglePurchaseCallback m_GooglePurchaseCallback;

		// Token: 0x04000029 RID: 41
		private readonly IQueryProductDetailsService m_QueryProductDetailsService;
	}
}
