using System;
using Unity.Multiplayer.Center.Common.Analytics;

namespace Unity.Multiplayer.Center.Common
{
	// Token: 0x0200000C RID: 12
	public interface ISectionWithAnalytics
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26
		// (set) Token: 0x0600001B RID: 27
		IOnboardingSectionAnalyticsProvider AnalyticsProvider { get; set; }
	}
}
