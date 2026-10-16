using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000209 RID: 521
	[Serializable]
	public class GetLeaderboardAroundCharacterResult : PlayFabResultCommon
	{
		// Token: 0x04000C0C RID: 3084
		public List<CharacterLeaderboardEntry> Leaderboard;
	}
}
