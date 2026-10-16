using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000263 RID: 611
	[Serializable]
	public class LinkGameCenterAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CD6 RID: 3286
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CD7 RID: 3287
		public bool? ForceLink;

		// Token: 0x04000CD8 RID: 3288
		public string GameCenterId;

		// Token: 0x04000CD9 RID: 3289
		public string PublicKeyUrl;

		// Token: 0x04000CDA RID: 3290
		public string Salt;

		// Token: 0x04000CDB RID: 3291
		public string Signature;

		// Token: 0x04000CDC RID: 3292
		public string Timestamp;
	}
}
