using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001FE RID: 510
	[Serializable]
	public class GetCharacterLeaderboardResult : PlayFabResultCommon
	{
		// Token: 0x04000BE6 RID: 3046
		public List<CharacterLeaderboardEntry> Leaderboard;
	}
}
