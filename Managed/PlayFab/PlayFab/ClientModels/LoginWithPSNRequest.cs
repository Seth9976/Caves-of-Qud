using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200028C RID: 652
	[Serializable]
	public class LoginWithPSNRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D93 RID: 3475
		public string AuthCode;

		// Token: 0x04000D94 RID: 3476
		public bool? CreateAccount;

		// Token: 0x04000D95 RID: 3477
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D96 RID: 3478
		public string EncryptedRequest;

		// Token: 0x04000D97 RID: 3479
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D98 RID: 3480
		public int? IssuerId;

		// Token: 0x04000D99 RID: 3481
		public string PlayerSecret;

		// Token: 0x04000D9A RID: 3482
		public string RedirectUri;

		// Token: 0x04000D9B RID: 3483
		public string TitleId;
	}
}
