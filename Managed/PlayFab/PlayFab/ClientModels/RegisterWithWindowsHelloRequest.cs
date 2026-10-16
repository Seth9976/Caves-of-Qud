using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002B1 RID: 689
	[Serializable]
	public class RegisterWithWindowsHelloRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E5E RID: 3678
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000E5F RID: 3679
		public string DeviceName;

		// Token: 0x04000E60 RID: 3680
		public string EncryptedRequest;

		// Token: 0x04000E61 RID: 3681
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000E62 RID: 3682
		public string PlayerSecret;

		// Token: 0x04000E63 RID: 3683
		public string PublicKey;

		// Token: 0x04000E64 RID: 3684
		public string TitleId;

		// Token: 0x04000E65 RID: 3685
		public string UserName;
	}
}
