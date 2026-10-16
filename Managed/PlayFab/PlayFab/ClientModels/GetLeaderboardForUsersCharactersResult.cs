using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200020D RID: 525
	[Serializable]
	public class GetLeaderboardForUsersCharactersResult : PlayFabResultCommon
	{
		// Token: 0x04000C18 RID: 3096
		public List<CharacterLeaderboardEntry> Leaderboard;
	}
}
