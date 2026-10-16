using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200026E RID: 622
	[Serializable]
	public class LinkOpenIdConnectRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CEF RID: 3311
		public string ConnectionId;

		// Token: 0x04000CF0 RID: 3312
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CF1 RID: 3313
		public bool? ForceLink;

		// Token: 0x04000CF2 RID: 3314
		public string IdToken;
	}
}
