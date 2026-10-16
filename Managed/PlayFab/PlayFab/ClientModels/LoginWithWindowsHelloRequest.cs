using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200028F RID: 655
	[Serializable]
	public class LoginWithWindowsHelloRequest : PlayFabRequestCommon
	{
		// Token: 0x04000DAA RID: 3498
		public string ChallengeSignature;

		// Token: 0x04000DAB RID: 3499
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000DAC RID: 3500
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000DAD RID: 3501
		public string PublicKeyHint;

		// Token: 0x04000DAE RID: 3502
		public string TitleId;
	}
}
