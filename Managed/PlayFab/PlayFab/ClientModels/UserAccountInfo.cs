using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000310 RID: 784
	[Serializable]
	public class UserAccountInfo : PlayFabBaseModel
	{
		// Token: 0x04000F4F RID: 3919
		public UserAndroidDeviceInfo AndroidDeviceInfo;

		// Token: 0x04000F50 RID: 3920
		public UserAppleIdInfo AppleAccountInfo;

		// Token: 0x04000F51 RID: 3921
		public DateTime Created;

		// Token: 0x04000F52 RID: 3922
		public UserCustomIdInfo CustomIdInfo;

		// Token: 0x04000F53 RID: 3923
		public UserFacebookInfo FacebookInfo;

		// Token: 0x04000F54 RID: 3924
		public UserFacebookInstantGamesIdInfo FacebookInstantGamesIdInfo;

		// Token: 0x04000F55 RID: 3925
		public UserGameCenterInfo GameCenterInfo;

		// Token: 0x04000F56 RID: 3926
		public UserGoogleInfo GoogleInfo;

		// Token: 0x04000F57 RID: 3927
		public UserIosDeviceInfo IosDeviceInfo;

		// Token: 0x04000F58 RID: 3928
		public UserKongregateInfo KongregateInfo;

		// Token: 0x04000F59 RID: 3929
		public UserNintendoSwitchAccountIdInfo NintendoSwitchAccountInfo;

		// Token: 0x04000F5A RID: 3930
		public UserNintendoSwitchDeviceIdInfo NintendoSwitchDeviceIdInfo;

		// Token: 0x04000F5B RID: 3931
		public List<UserOpenIdInfo> OpenIdInfo;

		// Token: 0x04000F5C RID: 3932
		public string PlayFabId;

		// Token: 0x04000F5D RID: 3933
		public UserPrivateAccountInfo PrivateInfo;

		// Token: 0x04000F5E RID: 3934
		public UserPsnInfo PsnInfo;

		// Token: 0x04000F5F RID: 3935
		public UserSteamInfo SteamInfo;

		// Token: 0x04000F60 RID: 3936
		public UserTitleInfo TitleInfo;

		// Token: 0x04000F61 RID: 3937
		public UserTwitchInfo TwitchInfo;

		// Token: 0x04000F62 RID: 3938
		public string Username;

		// Token: 0x04000F63 RID: 3939
		public UserWindowsHelloInfo WindowsHelloInfo;

		// Token: 0x04000F64 RID: 3940
		public UserXboxInfo XboxInfo;
	}
}
