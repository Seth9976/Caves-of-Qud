using System;
using System.Collections.ObjectModel;

namespace UnityEngine.Purchasing.Extension
{
	// Token: 0x02000049 RID: 73
	public interface IStore
	{
		// Token: 0x06000141 RID: 321
		void Initialize(IStoreCallback callback);

		// Token: 0x06000142 RID: 322
		void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products);

		// Token: 0x06000143 RID: 323
		void Purchase(ProductDefinition product, string developerPayload);

		// Token: 0x06000144 RID: 324
		void FinishTransaction(ProductDefinition product, string transactionId);
	}
}
