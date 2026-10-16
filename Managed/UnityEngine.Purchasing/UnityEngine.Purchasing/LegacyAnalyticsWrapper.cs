using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200000D RID: 13
	internal class LegacyAnalyticsWrapper : IAnalyticsAdapter, ICoreServicesEnvironmentObserver
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000222D File Offset: 0x0000042D
		internal LegacyAnalyticsWrapper(IAnalyticsAdapter legacyAdapter, IAnalyticsAdapter emptyAdapter)
		{
			this.m_LegacyAdapter = legacyAdapter;
			this.m_EmptyAdapter = emptyAdapter;
			CoreServicesEnvironmentSubject.Instance().SubscribeToUpdatesAndGetCurrent(this);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002255 File Offset: 0x00000455
		public void SendTransactionEvent(Product product)
		{
			this.m_AnalyticsAdapter.SendTransactionEvent(product);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002263 File Offset: 0x00000463
		public void SendTransactionFailedEvent(Product product, PurchaseFailureDescription description)
		{
			this.m_AnalyticsAdapter.SendTransactionFailedEvent(product, description);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002272 File Offset: 0x00000472
		public void OnUpdatedCoreServicesEnvironment(string currentEnvironment)
		{
			this.m_Enabled = CoreServicesEnvironmentSubject.Instance().IsDefaultLiveEnvironment(currentEnvironment);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002285 File Offset: 0x00000485
		private IAnalyticsAdapter m_AnalyticsAdapter
		{
			get
			{
				if (!this.m_Enabled)
				{
					return this.m_EmptyAdapter;
				}
				return this.m_LegacyAdapter;
			}
		}

		// Token: 0x04000006 RID: 6
		private bool m_Enabled = true;

		// Token: 0x04000007 RID: 7
		private IAnalyticsAdapter m_LegacyAdapter;

		// Token: 0x04000008 RID: 8
		private IAnalyticsAdapter m_EmptyAdapter;
	}
}
