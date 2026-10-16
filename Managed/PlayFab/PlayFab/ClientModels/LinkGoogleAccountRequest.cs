using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000265 RID: 613
	[Serializable]
	public class LinkGoogleAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CDD RID: 3293
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CDE RID: 3294
		public bool? ForceLink;

		// Token: 0x04000CDF RID: 3295
		public string ServerAuthCode;
	}
}
