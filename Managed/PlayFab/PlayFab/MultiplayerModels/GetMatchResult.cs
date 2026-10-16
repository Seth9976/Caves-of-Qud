using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000A3 RID: 163
	[Serializable]
	public class GetMatchResult : PlayFabResultCommon
	{
		// Token: 0x040004A3 RID: 1187
		public string MatchId;

		// Token: 0x040004A4 RID: 1188
		public List<MatchmakingPlayerWithTeamAssignment> Members;

		// Token: 0x040004A5 RID: 1189
		public List<string> RegionPreferences;

		// Token: 0x040004A6 RID: 1190
		public ServerDetails ServerDetails;
	}
}
