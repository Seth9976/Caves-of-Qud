using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000284 RID: 644
	[Serializable]
	public class LoginWithGameCenterRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D55 RID: 3413
		public bool? CreateAccount;

		// Token: 0x04000D56 RID: 3414
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D57 RID: 3415
		public string EncryptedRequest;

		// Token: 0x04000D58 RID: 3416
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D59 RID: 3417
		public string PlayerId;

		// Token: 0x04000D5A RID: 3418
		public string PlayerSecret;

		// Token: 0x04000D5B RID: 3419
		public string PublicKeyUrl;

		// Token: 0x04000D5C RID: 3420
		public string Salt;

		// Token: 0x04000D5D RID: 3421
		public string Signature;

		// Token: 0x04000D5E RID: 3422
		public string Timestamp;

		// Token: 0x04000D5F RID: 3423
		public string TitleId;
	}
}
