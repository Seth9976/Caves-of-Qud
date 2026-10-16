using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000008 RID: 8
	internal class EmptyAnalyticsAdapter : IAnalyticsAdapter
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000215A File Offset: 0x0000035A
		public void SendTransactionEvent(Product product)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000215C File Offset: 0x0000035C
		public void SendTransactionFailedEvent(Product product, PurchaseFailureDescription reason)
		{
		}
	}
}
