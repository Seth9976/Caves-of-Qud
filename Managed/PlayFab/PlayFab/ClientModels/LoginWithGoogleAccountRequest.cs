using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000285 RID: 645
	[Serializable]
	public class LoginWithGoogleAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D60 RID: 3424
		public bool? CreateAccount;

		// Token: 0x04000D61 RID: 3425
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D62 RID: 3426
		public string EncryptedRequest;

		// Token: 0x04000D63 RID: 3427
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D64 RID: 3428
		public string PlayerSecret;

		// Token: 0x04000D65 RID: 3429
		public string ServerAuthCode;

		// Token: 0x04000D66 RID: 3430
		public string TitleId;
	}
}
