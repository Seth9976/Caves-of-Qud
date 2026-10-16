using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200020C RID: 524
	[Serializable]
	public class GetLeaderboardForUsersCharactersRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C16 RID: 3094
		public int MaxResultsCount;

		// Token: 0x04000C17 RID: 3095
		public string StatisticName;
	}
}
