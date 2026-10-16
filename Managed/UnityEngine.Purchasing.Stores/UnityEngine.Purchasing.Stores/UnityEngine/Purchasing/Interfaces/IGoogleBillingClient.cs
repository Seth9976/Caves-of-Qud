using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x0200009B RID: 155
	internal interface IGoogleBillingClient
	{
		// Token: 0x0600037F RID: 895
		void StartConnection(IBillingClientStateListener billingClientStateListener);

		// Token: 0x06000380 RID: 896
		void EndConnection();

		// Token: 0x06000381 RID: 897
		bool IsReady();

		// Token: 0x06000382 RID: 898
		GoogleBillingConnectionState GetConnectionState();

		// Token: 0x06000383 RID: 899
		void QueryPurchasesAsync(string skuType, Action<IGoogleBillingResult, IEnumerable<AndroidJavaObject>> onQueryPurchasesResponse);

		// Token: 0x06000384 RID: 900
		void QueryProductDetailsAsync(List<string> skus, string type, Action<IGoogleBillingResult, List<AndroidJavaObject>> onProductDetailsResponseAction);

		// Token: 0x06000385 RID: 901
		AndroidJavaObject LaunchBillingFlow(AndroidJavaObject productDetails, string oldPurchaseToken, GooglePlayReplacementMode? replacementMode);

		// Token: 0x06000386 RID: 902
		void ConsumeAsync(string purchaseToken, Action<IGoogleBillingResult> onConsume);

		// Token: 0x06000387 RID: 903
		void AcknowledgePurchase(string purchaseToken, Action<IGoogleBillingResult> onAcknowledge);

		// Token: 0x06000388 RID: 904
		void SetObfuscationAccountId(string obfuscationAccountId);

		// Token: 0x06000389 RID: 905
		void SetObfuscationProfileId(string obfuscationProfileId);
	}
}
