using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001A RID: 26
	public interface IStoreController
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95
		ProductCollection products { get; }

		// Token: 0x06000060 RID: 96
		void InitiatePurchase(Product product, string payload);

		// Token: 0x06000061 RID: 97
		void InitiatePurchase(string productId, string payload);

		// Token: 0x06000062 RID: 98
		void InitiatePurchase(Product product);

		// Token: 0x06000063 RID: 99
		void InitiatePurchase(string productId);

		// Token: 0x06000064 RID: 100
		[Obsolete]
		void FetchAdditionalProducts(HashSet<ProductDefinition> additionalProducts, Action successCallback, Action<InitializationFailureReason> failCallback);

		// Token: 0x06000065 RID: 101
		void FetchAdditionalProducts(HashSet<ProductDefinition> additionalProducts, Action successCallback, Action<InitializationFailureReason, string> failCallback);

		// Token: 0x06000066 RID: 102
		void ConfirmPendingPurchase(Product product);
	}
}
