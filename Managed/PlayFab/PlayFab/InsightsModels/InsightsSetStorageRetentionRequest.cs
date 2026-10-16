using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x0200010E RID: 270
	[Serializable]
	public class InsightsSetStorageRetentionRequest : PlayFabRequestCommon
	{
		// Token: 0x040005F5 RID: 1525
		public Dictionary<string, string> CustomTags;

		// Token: 0x040005F6 RID: 1526
		public int RetentionDays;
	}
}
