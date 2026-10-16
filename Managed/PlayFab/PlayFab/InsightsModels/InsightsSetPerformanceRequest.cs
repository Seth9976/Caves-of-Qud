using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x0200010D RID: 269
	[Serializable]
	public class InsightsSetPerformanceRequest : PlayFabRequestCommon
	{
		// Token: 0x040005F3 RID: 1523
		public Dictionary<string, string> CustomTags;

		// Token: 0x040005F4 RID: 1524
		public int PerformanceLevel;
	}
}
