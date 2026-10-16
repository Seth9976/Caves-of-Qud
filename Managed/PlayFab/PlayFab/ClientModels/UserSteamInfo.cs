using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000323 RID: 803
	[Serializable]
	public class UserSteamInfo : PlayFabBaseModel
	{
		// Token: 0x04000F9E RID: 3998
		public TitleActivationStatus? SteamActivationStatus;

		// Token: 0x04000F9F RID: 3999
		public string SteamCountry;

		// Token: 0x04000FA0 RID: 4000
		public Currency? SteamCurrency;

		// Token: 0x04000FA1 RID: 4001
		public string SteamId;

		// Token: 0x04000FA2 RID: 4002
		public string SteamName;
	}
}
