using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000289 RID: 649
	[Serializable]
	public class LoginWithNintendoSwitchDeviceIdRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D7F RID: 3455
		public bool? CreateAccount;

		// Token: 0x04000D80 RID: 3456
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D81 RID: 3457
		public string EncryptedRequest;

		// Token: 0x04000D82 RID: 3458
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D83 RID: 3459
		public string NintendoSwitchDeviceId;

		// Token: 0x04000D84 RID: 3460
		public string PlayerSecret;

		// Token: 0x04000D85 RID: 3461
		public string TitleId;
	}
}
