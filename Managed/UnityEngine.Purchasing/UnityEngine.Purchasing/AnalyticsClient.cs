using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000007 RID: 7
	internal class AnalyticsClient : IAnalyticsClient
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002100 File Offset: 0x00000300
		public AnalyticsClient(IAnalyticsAdapter analytics, IAnalyticsAdapter legacyAnalytics)
		{
			this.m_Analytics = analytics;
			this.m_LegacyAnalytics = legacyAnalytics;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002116 File Offset: 0x00000316
		public void OnPurchaseSucceeded(Product product)
		{
			if (product.metadata.isoCurrencyCode == null)
			{
				return;
			}
			this.m_Analytics.SendTransactionEvent(product);
			this.m_LegacyAnalytics.SendTransactionEvent(product);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000213E File Offset: 0x0000033E
		public void OnPurchaseFailed(Product product, PurchaseFailureDescription description)
		{
			this.m_Analytics.SendTransactionFailedEvent(product, description);
			this.m_LegacyAnalytics.SendTransactionFailedEvent(product, description);
		}

		// Token: 0x04000003 RID: 3
		private readonly IAnalyticsAdapter m_Analytics;

		// Token: 0x04000004 RID: 4
		private readonly IAnalyticsAdapter m_LegacyAnalytics;
	}
}
