using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002E8 RID: 744
	[Serializable]
	public class UnlinkFacebookInstantGamesIdRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F16 RID: 3862
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F17 RID: 3863
		public string FacebookInstantGamesId;
	}
}
