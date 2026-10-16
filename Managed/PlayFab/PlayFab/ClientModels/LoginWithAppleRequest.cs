using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200027F RID: 639
	[Serializable]
	public class LoginWithAppleRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D34 RID: 3380
		public bool? CreateAccount;

		// Token: 0x04000D35 RID: 3381
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D36 RID: 3382
		public string EncryptedRequest;

		// Token: 0x04000D37 RID: 3383
		public string IdentityToken;

		// Token: 0x04000D38 RID: 3384
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D39 RID: 3385
		public string PlayerSecret;

		// Token: 0x04000D3A RID: 3386
		public string TitleId;
	}
}
