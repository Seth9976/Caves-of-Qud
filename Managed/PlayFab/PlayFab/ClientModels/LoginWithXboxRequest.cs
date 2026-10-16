using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000290 RID: 656
	[Serializable]
	public class LoginWithXboxRequest : PlayFabRequestCommon
	{
		// Token: 0x04000DAF RID: 3503
		public bool? CreateAccount;

		// Token: 0x04000DB0 RID: 3504
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000DB1 RID: 3505
		public string EncryptedRequest;

		// Token: 0x04000DB2 RID: 3506
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000DB3 RID: 3507
		public string PlayerSecret;

		// Token: 0x04000DB4 RID: 3508
		public string TitleId;

		// Token: 0x04000DB5 RID: 3509
		public string XboxToken;
	}
}
