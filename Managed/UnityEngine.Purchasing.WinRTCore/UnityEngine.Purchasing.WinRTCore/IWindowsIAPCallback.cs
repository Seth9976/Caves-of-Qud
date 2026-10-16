using System;

namespace UnityEngine.Purchasing.Default
{
	// Token: 0x02000003 RID: 3
	public interface IWindowsIAPCallback
	{
		// Token: 0x06000003 RID: 3
		void OnProductListReceived(WinProductDescription[] winProducts);

		// Token: 0x06000004 RID: 4
		void OnProductListError(string message);

		// Token: 0x06000005 RID: 5
		void OnPurchaseSucceeded(string productId, string receipt, string transactionId);

		// Token: 0x06000006 RID: 6
		void OnPurchaseFailed(string productId, string error);

		// Token: 0x06000007 RID: 7
		void log(string message);

		// Token: 0x06000008 RID: 8
		void logError(string error);
	}
}
