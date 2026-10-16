using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200025F RID: 607
	[Serializable]
	public class LinkFacebookAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CD0 RID: 3280
		public string AccessToken;

		// Token: 0x04000CD1 RID: 3281
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CD2 RID: 3282
		public bool? ForceLink;
	}
}
