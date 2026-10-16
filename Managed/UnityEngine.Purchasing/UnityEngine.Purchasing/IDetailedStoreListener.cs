using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000017 RID: 23
	public interface IDetailedStoreListener : IStoreListener
	{
		// Token: 0x06000058 RID: 88
		void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription);
	}
}
