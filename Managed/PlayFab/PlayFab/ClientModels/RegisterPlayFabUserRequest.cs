using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002AF RID: 687
	[Serializable]
	public class RegisterPlayFabUserRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E4F RID: 3663
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000E50 RID: 3664
		public string DisplayName;

		// Token: 0x04000E51 RID: 3665
		public string Email;

		// Token: 0x04000E52 RID: 3666
		public string EncryptedRequest;

		// Token: 0x04000E53 RID: 3667
		public GetPlayerCombinedInfoRequestParams InfoRequestParameters;

		// Token: 0x04000E54 RID: 3668
		public string Password;

		// Token: 0x04000E55 RID: 3669
		public string PlayerSecret;

		// Token: 0x04000E56 RID: 3670
		public bool? RequireBothUsernameAndEmail;

		// Token: 0x04000E57 RID: 3671
		public string TitleId;

		// Token: 0x04000E58 RID: 3672
		public string Username;
	}
}
