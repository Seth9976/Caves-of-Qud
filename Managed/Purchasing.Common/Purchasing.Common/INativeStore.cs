using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000003 RID: 3
	public interface INativeStore
	{
		// Token: 0x06000003 RID: 3
		void RetrieveProducts(string json);

		// Token: 0x06000004 RID: 4
		void Purchase(string productJSON, string developerPayload);

		// Token: 0x06000005 RID: 5
		void FinishTransaction(string productJSON, string transactionID);
	}
}
