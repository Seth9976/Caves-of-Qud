using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	public class GetLeaderboardAroundPlayerRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C0D RID: 3085
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000C0E RID: 3086
		public int? MaxResultsCount;

		// Token: 0x04000C0F RID: 3087
		public string PlayFabId;

		// Token: 0x04000C10 RID: 3088
		public PlayerProfileViewConstraints ProfileConstraints;

		// Token: 0x04000C11 RID: 3089
		public string StatisticName;

		// Token: 0x04000C12 RID: 3090
		public int? Version;
	}
}
