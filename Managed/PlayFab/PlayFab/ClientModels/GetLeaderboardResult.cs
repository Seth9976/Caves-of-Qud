using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200020F RID: 527
	[Serializable]
	public class GetLeaderboardResult : PlayFabResultCommon
	{
		// Token: 0x04000C1F RID: 3103
		public List<PlayerLeaderboardEntry> Leaderboard;

		// Token: 0x04000C20 RID: 3104
		public DateTime? NextReset;

		// Token: 0x04000C21 RID: 3105
		public int Version;
	}
}
