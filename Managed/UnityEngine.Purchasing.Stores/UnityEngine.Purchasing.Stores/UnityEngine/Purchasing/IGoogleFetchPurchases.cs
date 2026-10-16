using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000033 RID: 51
	internal interface IGoogleFetchPurchases
	{
		// Token: 0x06000109 RID: 265
		void SetStoreCallback(IStoreCallback storeCallback);

		// Token: 0x0600010A RID: 266
		void FetchPurchases();

		// Token: 0x0600010B RID: 267
		void FetchPurchases(Action<List<Product>> onQueryPurchaseSucceed);
	}
}
