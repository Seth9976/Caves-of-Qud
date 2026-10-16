using System;
using System.Collections.ObjectModel;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000046 RID: 70
	internal interface INativeUDPStore : INativeStore
	{
		// Token: 0x06000174 RID: 372
		void Initialize(Action<bool, string> callback);

		// Token: 0x06000175 RID: 373
		void Purchase(string productId, Action<bool, string> callback, string developerPayload = null);

		// Token: 0x06000176 RID: 374
		void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products, Action<bool, string> callback);

		// Token: 0x06000177 RID: 375
		void FinishTransaction(ProductDefinition productDefinition, string transactionID);
	}
}
