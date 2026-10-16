using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000089 RID: 137
	public interface ITransactionHistoryExtensions : IStoreExtension
	{
		// Token: 0x06000325 RID: 805
		PurchaseFailureDescription GetLastPurchaseFailureDescription();

		// Token: 0x06000326 RID: 806
		StoreSpecificPurchaseErrorCode GetLastStoreSpecificPurchaseErrorCode();
	}
}
