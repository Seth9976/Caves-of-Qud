using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200020E RID: 526
	[Serializable]
	public class GetLeaderboardRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C19 RID: 3097
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000C1A RID: 3098
		public int? MaxResultsCount;

		// Token: 0x04000C1B RID: 3099
		public PlayerProfileViewConstraints ProfileConstraints;

		// Token: 0x04000C1C RID: 3100
		public int StartPosition;

		// Token: 0x04000C1D RID: 3101
		public string StatisticName;

		// Token: 0x04000C1E RID: 3102
		public int? Version;
	}
}
