using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002B0 RID: 688
	[Serializable]
	public class RegisterPlayFabUserResult : PlayFabLoginResultCommon
	{
		// Token: 0x04000E59 RID: 3673
		public EntityTokenResponse EntityToken;

		// Token: 0x04000E5A RID: 3674
		public string PlayFabId;

		// Token: 0x04000E5B RID: 3675
		public string SessionTicket;

		// Token: 0x04000E5C RID: 3676
		public UserSettings SettingsForUser;

		// Token: 0x04000E5D RID: 3677
		public string Username;
	}
}
