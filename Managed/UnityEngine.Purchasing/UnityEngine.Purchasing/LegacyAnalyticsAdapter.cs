using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200000C RID: 12
	internal class LegacyAnalyticsAdapter : IAnalyticsAdapter
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002166 File Offset: 0x00000366
		public LegacyAnalyticsAdapter(ILegacyUnityAnalytics legacyAnalytics)
		{
			this.m_LegacyAnalytics = legacyAnalytics;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002175 File Offset: 0x00000375
		public void SendTransactionEvent(Product product)
		{
			this.m_LegacyAnalytics.SendTransactionEvent(product.definition.storeSpecificId, product.metadata.localizedPrice, product.metadata.isoCurrencyCode, product.receipt, null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000021AC File Offset: 0x000003AC
		public void SendTransactionFailedEvent(Product product, PurchaseFailureDescription description)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"productID",
					product.definition.storeSpecificId
				},
				{ "reason", description.reason },
				{
					"price",
					product.metadata.localizedPrice
				},
				{
					"currency",
					product.metadata.isoCurrencyCode
				}
			};
			this.m_LegacyAnalytics.SendCustomEvent("unity.PurchaseFailed", dictionary);
		}

		// Token: 0x04000005 RID: 5
		private readonly ILegacyUnityAnalytics m_LegacyAnalytics;
	}
}
