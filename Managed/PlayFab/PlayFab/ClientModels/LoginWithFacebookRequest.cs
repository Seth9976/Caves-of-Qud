using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000283 RID: 643
	[Serializable]
	public class LoginWithFacebookRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D4E RID: 3406
		public string AccessToken;

		// Token: 0x04000D4F RID: 3407
		public bool? CreateAccount;

		// Token: 0x04000D50 RID: 3408
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D51 RID: 3409
		public string EncryptedRequest;

		// Token: 0x04000D52 RID: 3410
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D53 RID: 3411
		public string PlayerSecret;

		// Token: 0x04000D54 RID: 3412
		public string TitleId;
	}
}
