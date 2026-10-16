using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200000B RID: 11
	internal interface ILegacyUnityAnalytics
	{
		// Token: 0x0600002A RID: 42
		void SendTransactionEvent(string productId, decimal amount, string currency, string receiptPurchaseData, string signature);

		// Token: 0x0600002B RID: 43
		void SendCustomEvent(string name, Dictionary<string, object> data);
	}
}
