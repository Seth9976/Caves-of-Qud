using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200025E RID: 606
	[Serializable]
	public class LinkedPlatformAccountModel : PlayFabBaseModel
	{
		// Token: 0x04000CCC RID: 3276
		public string Email;

		// Token: 0x04000CCD RID: 3277
		public LoginIdentityProvider? Platform;

		// Token: 0x04000CCE RID: 3278
		public string PlatformUserId;

		// Token: 0x04000CCF RID: 3279
		public string Username;
	}
}
