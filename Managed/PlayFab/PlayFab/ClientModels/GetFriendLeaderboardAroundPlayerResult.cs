using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000204 RID: 516
	[Serializable]
	public class GetFriendLeaderboardAroundPlayerResult : PlayFabResultCommon
	{
		// Token: 0x04000BF6 RID: 3062
		public List<PlayerLeaderboardEntry> Leaderboard;

		// Token: 0x04000BF7 RID: 3063
		public DateTime? NextReset;

		// Token: 0x04000BF8 RID: 3064
		public int Version;
	}
}
