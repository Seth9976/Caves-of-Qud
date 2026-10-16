using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000220 RID: 544
	[Serializable]
	public class GetPlayerTagsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C54 RID: 3156
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000C55 RID: 3157
		public string Namespace;

		// Token: 0x04000C56 RID: 3158
		public string PlayFabId;
	}
}
