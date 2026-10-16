using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000280 RID: 640
	[Serializable]
	public class LoginWithCustomIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D3B RID: 3387
		public bool? CreateAccount;

		// Token: 0x04000D3C RID: 3388
		public string CustomId;

		// Token: 0x04000D3D RID: 3389
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D3E RID: 3390
		public string EncryptedRequest;

		// Token: 0x04000D3F RID: 3391
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D40 RID: 3392
		public string PlayerSecret;

		// Token: 0x04000D41 RID: 3393
		public string TitleId;
	}
}
