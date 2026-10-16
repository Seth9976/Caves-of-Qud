using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000287 RID: 647
	[Serializable]
	public class LoginWithKongregateRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D70 RID: 3440
		public string AuthTicket;

		// Token: 0x04000D71 RID: 3441
		public bool? CreateAccount;

		// Token: 0x04000D72 RID: 3442
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D73 RID: 3443
		public string EncryptedRequest;

		// Token: 0x04000D74 RID: 3444
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D75 RID: 3445
		public string KongregateId;

		// Token: 0x04000D76 RID: 3446
		public string PlayerSecret;

		// Token: 0x04000D77 RID: 3447
		public string TitleId;
	}
}
