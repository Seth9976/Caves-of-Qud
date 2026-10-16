using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000208 RID: 520
	[Serializable]
	public class GetLeaderboardAroundCharacterRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C08 RID: 3080
		public string CharacterId;

		// Token: 0x04000C09 RID: 3081
		public string CharacterType;

		// Token: 0x04000C0A RID: 3082
		public int? MaxResultsCount;

		// Token: 0x04000C0B RID: 3083
		public string StatisticName;
	}
}
