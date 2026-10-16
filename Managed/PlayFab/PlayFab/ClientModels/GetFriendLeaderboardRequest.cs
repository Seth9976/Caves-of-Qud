using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000205 RID: 517
	[Serializable]
	public class GetFriendLeaderboardRequest : PlayFabRequestCommon
	{
		// Token: 0x04000BF9 RID: 3065
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000BFA RID: 3066
		public bool? IncludeFacebookFriends;

		// Token: 0x04000BFB RID: 3067
		public bool? IncludeSteamFriends;

		// Token: 0x04000BFC RID: 3068
		public int? MaxResultsCount;

		// Token: 0x04000BFD RID: 3069
		public PlayerProfileViewConstraints ProfileConstraints;

		// Token: 0x04000BFE RID: 3070
		public int StartPosition;

		// Token: 0x04000BFF RID: 3071
		public string StatisticName;

		// Token: 0x04000C00 RID: 3072
		public int? Version;

		// Token: 0x04000C01 RID: 3073
		public string XboxToken;
	}
}
