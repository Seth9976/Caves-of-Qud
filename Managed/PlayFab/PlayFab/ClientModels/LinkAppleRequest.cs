using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200025B RID: 603
	[Serializable]
	public class LinkAppleRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CC6 RID: 3270
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CC7 RID: 3271
		public bool? ForceLink;

		// Token: 0x04000CC8 RID: 3272
		public string IdentityToken;
	}
}
