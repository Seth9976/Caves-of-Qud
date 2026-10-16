using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002C3 RID: 707
	[Serializable]
	public class SendAccountRecoveryEmailRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E84 RID: 3716
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000E85 RID: 3717
		public string Email;

		// Token: 0x04000E86 RID: 3718
		public string EmailTemplateId;

		// Token: 0x04000E87 RID: 3719
		public string TitleId;
	}
}
