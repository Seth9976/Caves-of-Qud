using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200028D RID: 653
	[Serializable]
	public class LoginWithSteamRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D9C RID: 3484
		public bool? CreateAccount;

		// Token: 0x04000D9D RID: 3485
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D9E RID: 3486
		public string EncryptedRequest;

		// Token: 0x04000D9F RID: 3487
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000DA0 RID: 3488
		public string PlayerSecret;

		// Token: 0x04000DA1 RID: 3489
		public string SteamTicket;

		// Token: 0x04000DA2 RID: 3490
		public string TitleId;
	}
}
