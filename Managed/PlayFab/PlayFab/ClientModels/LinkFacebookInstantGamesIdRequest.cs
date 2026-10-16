using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000261 RID: 609
	[Serializable]
	public class LinkFacebookInstantGamesIdRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CD3 RID: 3283
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CD4 RID: 3284
		public string FacebookInstantGamesSignature;

		// Token: 0x04000CD5 RID: 3285
		public bool? ForceLink;
	}
}
