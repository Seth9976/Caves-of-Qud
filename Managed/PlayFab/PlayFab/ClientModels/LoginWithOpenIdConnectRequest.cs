using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200028A RID: 650
	[Serializable]
	public class LoginWithOpenIdConnectRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D86 RID: 3462
		public string ConnectionId;

		// Token: 0x04000D87 RID: 3463
		public bool? CreateAccount;

		// Token: 0x04000D88 RID: 3464
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D89 RID: 3465
		public string EncryptedRequest;

		// Token: 0x04000D8A RID: 3466
		public string IdToken;

		// Token: 0x04000D8B RID: 3467
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D8C RID: 3468
		public string PlayerSecret;

		// Token: 0x04000D8D RID: 3469
		public string TitleId;
	}
}
