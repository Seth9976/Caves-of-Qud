using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000281 RID: 641
	[Serializable]
	public class LoginWithEmailAddressRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D42 RID: 3394
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D43 RID: 3395
		public string Email;

		// Token: 0x04000D44 RID: 3396
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000D45 RID: 3397
		public string Password;

		// Token: 0x04000D46 RID: 3398
		public string TitleId;
	}
}
