using System;
using System.Collections.Generic;
using UnityEngine.Analytics;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200000E RID: 14
	internal class LegacyUnityAnalytics : ILegacyUnityAnalytics
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000229C File Offset: 0x0000049C
		public void SendTransactionEvent(string productId, decimal amount, string currency, string receiptPurchaseData, string signature)
		{
			Analytics.Transaction(productId, amount, currency, receiptPurchaseData, signature);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000022AB File Offset: 0x000004AB
		public void SendCustomEvent(string name, Dictionary<string, object> data)
		{
			Analytics.CustomEvent(name, data);
		}
	}
}
