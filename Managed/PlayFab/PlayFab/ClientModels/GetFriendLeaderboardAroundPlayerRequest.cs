using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000203 RID: 515
	[Serializable]
	public class GetFriendLeaderboardAroundPlayerRequest : PlayFabRequestCommon
	{
		// Token: 0x04000BED RID: 3053
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000BEE RID: 3054
		public bool? IncludeFacebookFriends;

		// Token: 0x04000BEF RID: 3055
		public bool? IncludeSteamFriends;

		// Token: 0x04000BF0 RID: 3056
		public int? MaxResultsCount;

		// Token: 0x04000BF1 RID: 3057
		public string PlayFabId;

		// Token: 0x04000BF2 RID: 3058
		public PlayerProfileViewConstraints ProfileConstraints;

		// Token: 0x04000BF3 RID: 3059
		public string StatisticName;

		// Token: 0x04000BF4 RID: 3060
		public int? Version;

		// Token: 0x04000BF5 RID: 3061
		public string XboxToken;
	}
}
