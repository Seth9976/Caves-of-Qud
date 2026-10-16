using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000009 RID: 9
	internal interface IAnalyticsAdapter
	{
		// Token: 0x06000026 RID: 38
		void SendTransactionEvent(Product product);

		// Token: 0x06000027 RID: 39
		void SendTransactionFailedEvent(Product product, PurchaseFailureDescription description);
	}
}
