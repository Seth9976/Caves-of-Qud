using System;
using System.Collections.ObjectModel;

namespace UnityEngine.Purchasing.Extension
{
	// Token: 0x02000045 RID: 69
	public abstract class AbstractStore : IStore
	{
		// Token: 0x06000135 RID: 309
		public abstract void Initialize(IStoreCallback callback);

		// Token: 0x06000136 RID: 310
		public abstract void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products);

		// Token: 0x06000137 RID: 311
		public abstract void Purchase(ProductDefinition product, string developerPayload);

		// Token: 0x06000138 RID: 312
		public abstract void FinishTransaction(ProductDefinition product, string transactionId);
	}
}
