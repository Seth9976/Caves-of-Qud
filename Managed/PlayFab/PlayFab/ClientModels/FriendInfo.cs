using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001EB RID: 491
	[Serializable]
	public class FriendInfo : PlayFabBaseModel
	{
		// Token: 0x04000BA4 RID: 2980
		public UserFacebookInfo FacebookInfo;

		// Token: 0x04000BA5 RID: 2981
		public string FriendPlayFabId;

		// Token: 0x04000BA6 RID: 2982
		public UserGameCenterInfo GameCenterInfo;

		// Token: 0x04000BA7 RID: 2983
		public PlayerProfileModel Profile;

		// Token: 0x04000BA8 RID: 2984
		public UserPsnInfo PSNInfo;

		// Token: 0x04000BA9 RID: 2985
		public UserSteamInfo SteamInfo;

		// Token: 0x04000BAA RID: 2986
		public List<string> Tags;

		// Token: 0x04000BAB RID: 2987
		public string TitleDisplayName;

		// Token: 0x04000BAC RID: 2988
		public string Username;

		// Token: 0x04000BAD RID: 2989
		public UserXboxInfo XboxInfo;
	}
}
