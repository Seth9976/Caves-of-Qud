using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000288 RID: 648
	[Serializable]
	public class LoginWithNintendoServiceAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D78 RID: 3448
		public bool? CreateAccount;

		// Token: 0x04000D79 RID: 3449
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D7A RID: 3450
		public string EncryptedRequest;

		// Token: 0x04000D7B RID: 3451
		public string IdentityToken;

		// Token: 0x04000D7C RID: 3452
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D7D RID: 3453
		public string PlayerSecret;

		// Token: 0x04000D7E RID: 3454
		public string TitleId;
	}
}
