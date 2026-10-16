using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200027E RID: 638
	[Serializable]
	public class LoginWithAndroidDeviceIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D2B RID: 3371
		public string AndroidDevice;

		// Token: 0x04000D2C RID: 3372
		public string AndroidDeviceId;

		// Token: 0x04000D2D RID: 3373
		public bool? CreateAccount;

		// Token: 0x04000D2E RID: 3374
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D2F RID: 3375
		public string EncryptedRequest;

		// Token: 0x04000D30 RID: 3376
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D31 RID: 3377
		public string OS;

		// Token: 0x04000D32 RID: 3378
		public string PlayerSecret;

		// Token: 0x04000D33 RID: 3379
		public string TitleId;
	}
}
