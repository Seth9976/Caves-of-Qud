using System;
using System.Collections.ObjectModel;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000036 RID: 54
	internal interface IGooglePlayStoreRetrieveProductsService
	{
		// Token: 0x0600010F RID: 271
		void SetStoreCallback(IStoreCallback storeCallback);

		// Token: 0x06000110 RID: 272
		void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products, bool wantPurchases);

		// Token: 0x06000111 RID: 273
		void ResumeConnection();

		// Token: 0x06000112 RID: 274
		bool HasInitiallyRetrievedProducts();
	}
}
