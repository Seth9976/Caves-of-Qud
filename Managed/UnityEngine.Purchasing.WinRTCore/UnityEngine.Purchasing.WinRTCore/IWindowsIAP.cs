using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing.Default
{
	// Token: 0x02000004 RID: 4
	public interface IWindowsIAP
	{
		// Token: 0x06000009 RID: 9
		void BuildDummyProducts(List<WinProductDescription> products);

		// Token: 0x0600000A RID: 10
		void Initialize(IWindowsIAPCallback callback);

		// Token: 0x0600000B RID: 11
		void RetrieveProducts(bool retryIfOffline);

		// Token: 0x0600000C RID: 12
		void Purchase(string productId);

		// Token: 0x0600000D RID: 13
		void FinaliseTransaction(string transactionId);
	}
}
