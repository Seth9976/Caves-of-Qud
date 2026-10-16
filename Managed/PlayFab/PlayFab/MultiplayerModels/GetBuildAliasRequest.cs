using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public class GetBuildAliasRequest : PlayFabRequestCommon
	{
		// Token: 0x04000471 RID: 1137
		public string AliasId;

		// Token: 0x04000472 RID: 1138
		public Dictionary<string, string> CustomTags;
	}
}
