using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000185 RID: 389
	[Serializable]
	public class LinkedPlatformAccountModel : PlayFabBaseModel
	{
		// Token: 0x0400088B RID: 2187
		public string Email;

		// Token: 0x0400088C RID: 2188
		public LoginIdentityProvider? Platform;

		// Token: 0x0400088D RID: 2189
		public string PlatformUserId;

		// Token: 0x0400088E RID: 2190
		public string Username;
	}
}
