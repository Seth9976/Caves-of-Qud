using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000034 RID: 52
	internal interface IGooglePlayStoreFinishTransactionService
	{
		// Token: 0x0600010C RID: 268
		void SetStoreCallback(IStoreCallback storeCallback);

		// Token: 0x0600010D RID: 269
		void FinishTransaction(ProductDefinition product, string purchaseToken);
	}
}
