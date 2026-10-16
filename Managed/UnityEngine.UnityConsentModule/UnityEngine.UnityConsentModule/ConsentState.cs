using System;

namespace UnityEngine.UnityConsent
{
	// Token: 0x02000002 RID: 2
	public struct ConsentState
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ConsentState()
		{
			this.AdsIntent = ConsentStatus.Unspecified;
			this.AnalyticsIntent = ConsentStatus.Unspecified;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}", new object[] { "AdsIntent", this.AdsIntent, "AnalyticsIntent", this.AnalyticsIntent });
		}

		// Token: 0x04000001 RID: 1
		public ConsentStatus AdsIntent;

		// Token: 0x04000002 RID: 2
		public ConsentStatus AnalyticsIntent;
	}
}
