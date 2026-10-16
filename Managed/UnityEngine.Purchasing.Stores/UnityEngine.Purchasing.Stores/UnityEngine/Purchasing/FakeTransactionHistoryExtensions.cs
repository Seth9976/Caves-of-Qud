using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000088 RID: 136
	internal class FakeTransactionHistoryExtensions : ITransactionHistoryExtensions, IStoreExtension
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public PurchaseFailureDescription GetLastPurchaseFailureDescription()
		{
			return null;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A9B9 File Offset: 0x00008BB9
		public StoreSpecificPurchaseErrorCode GetLastStoreSpecificPurchaseErrorCode()
		{
			return StoreSpecificPurchaseErrorCode.Unknown;
		}
	}
}
