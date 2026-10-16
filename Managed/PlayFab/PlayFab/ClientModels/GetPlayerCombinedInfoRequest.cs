using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000214 RID: 532
	[Serializable]
	public class GetPlayerCombinedInfoRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C27 RID: 3111
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000C28 RID: 3112
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000C29 RID: 3113
		public string PlayFabId;
	}
}
