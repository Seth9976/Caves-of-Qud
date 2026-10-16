using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	public class GetLeaderboardAroundPlayerResult : PlayFabResultCommon
	{
		// Token: 0x04000C13 RID: 3091
		public List<PlayerLeaderboardEntry> Leaderboard;

		// Token: 0x04000C14 RID: 3092
		public DateTime? NextReset;

		// Token: 0x04000C15 RID: 3093
		public int Version;
	}
}
