using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000286 RID: 646
	[Serializable]
	public class LoginWithIOSDeviceIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D67 RID: 3431
		public bool? CreateAccount;

		// Token: 0x04000D68 RID: 3432
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D69 RID: 3433
		public string DeviceId;

		// Token: 0x04000D6A RID: 3434
		public string DeviceModel;

		// Token: 0x04000D6B RID: 3435
		public string EncryptedRequest;

		// Token: 0x04000D6C RID: 3436
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D6D RID: 3437
		public string OS;

		// Token: 0x04000D6E RID: 3438
		public string PlayerSecret;

		// Token: 0x04000D6F RID: 3439
		public string TitleId;
	}
}
