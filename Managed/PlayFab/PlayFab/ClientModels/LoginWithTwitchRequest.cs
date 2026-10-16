using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200028E RID: 654
	[Serializable]
	public class LoginWithTwitchRequest : PlayFabRequestCommon
	{
		// Token: 0x04000DA3 RID: 3491
		public string AccessToken;

		// Token: 0x04000DA4 RID: 3492
		public bool? CreateAccount;

		// Token: 0x04000DA5 RID: 3493
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000DA6 RID: 3494
		public string EncryptedRequest;

		// Token: 0x04000DA7 RID: 3495
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000DA8 RID: 3496
		public string PlayerSecret;

		// Token: 0x04000DA9 RID: 3497
		public string TitleId;
	}
}
