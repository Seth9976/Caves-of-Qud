using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001BA RID: 442
	[Serializable]
	public class AddUsernamePasswordRequest : PlayFabRequestCommon
	{
		// Token: 0x0400096A RID: 2410
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400096B RID: 2411
		public string Email;

		// Token: 0x0400096C RID: 2412
		public string Password;

		// Token: 0x0400096D RID: 2413
		public string Username;
	}
}
