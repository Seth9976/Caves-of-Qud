using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000018 RID: 24
	internal interface IGoogleCachedQueryProductDetailsService
	{
		// Token: 0x06000063 RID: 99
		IEnumerable<AndroidJavaObject> GetCachedQueriedProducts();

		// Token: 0x06000064 RID: 100
		IEnumerable<AndroidJavaObject> GetCachedQueriedProductDetails(IEnumerable<ProductDefinition> products);

		// Token: 0x06000065 RID: 101
		bool Contains(ProductDefinition products);

		// Token: 0x06000066 RID: 102
		void AddCachedQueriedProductDetails(IEnumerable<AndroidJavaObject> queriedProducts);
	}
}
