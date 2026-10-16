using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001F3 RID: 499
	[Serializable]
	public class GetAccountInfoRequest : PlayFabRequestCommon
	{
		// Token: 0x04000BCA RID: 3018
		public string Email;

		// Token: 0x04000BCB RID: 3019
		public string PlayFabId;

		// Token: 0x04000BCC RID: 3020
		public string TitleDisplayName;

		// Token: 0x04000BCD RID: 3021
		public string Username;
	}
}
