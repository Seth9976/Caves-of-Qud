using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001FD RID: 509
	[Serializable]
	public class GetCharacterLeaderboardRequest : PlayFabRequestCommon
	{
		// Token: 0x04000BE2 RID: 3042
		public string CharacterType;

		// Token: 0x04000BE3 RID: 3043
		public int? MaxResultsCount;

		// Token: 0x04000BE4 RID: 3044
		public int StartPosition;

		// Token: 0x04000BE5 RID: 3045
		public string StatisticName;
	}
}
