using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public class GetTitlePlayersFromMasterPlayerAccountIdsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000324 RID: 804
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000325 RID: 805
		public List<string> MasterPlayerAccountIds;

		// Token: 0x04000326 RID: 806
		public string TitleId;
	}
}
