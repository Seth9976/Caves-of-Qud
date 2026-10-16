using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000282 RID: 642
	[Serializable]
	public class LoginWithFacebookInstantGamesIdRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D47 RID: 3399
		public bool? CreateAccount;

		// Token: 0x04000D48 RID: 3400
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D49 RID: 3401
		public string EncryptedRequest;

		// Token: 0x04000D4A RID: 3402
		public string FacebookInstantGamesSignature;

		// Token: 0x04000D4B RID: 3403
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D4C RID: 3404
		public string PlayerSecret;

		// Token: 0x04000D4D RID: 3405
		public string TitleId;
	}
}
