using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200026F RID: 623
	[Serializable]
	public class LinkPSNAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CF3 RID: 3315
		public string AuthCode;

		// Token: 0x04000CF4 RID: 3316
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CF5 RID: 3317
		public bool? ForceLink;

		// Token: 0x04000CF6 RID: 3318
		public int? IssuerId;

		// Token: 0x04000CF7 RID: 3319
		public string RedirectUri;
	}
}
