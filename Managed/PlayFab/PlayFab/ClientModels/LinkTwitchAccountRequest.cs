using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000273 RID: 627
	[Serializable]
	public class LinkTwitchAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CFB RID: 3323
		public string AccessToken;

		// Token: 0x04000CFC RID: 3324
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CFD RID: 3325
		public bool? ForceLink;
	}
}
