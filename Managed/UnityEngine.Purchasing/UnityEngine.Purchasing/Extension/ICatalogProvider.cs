using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing.Extension
{
	// Token: 0x02000046 RID: 70
	public interface ICatalogProvider
	{
		// Token: 0x0600013A RID: 314
		void FetchProducts(Action<HashSet<ProductDefinition>> callback);
	}
}
