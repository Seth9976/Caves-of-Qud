using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000324 RID: 804
	[Serializable]
	public class UserTitleInfo : PlayFabBaseModel
	{
		// Token: 0x04000FA3 RID: 4003
		public string AvatarUrl;

		// Token: 0x04000FA4 RID: 4004
		public DateTime Created;

		// Token: 0x04000FA5 RID: 4005
		public string DisplayName;

		// Token: 0x04000FA6 RID: 4006
		public DateTime? FirstLogin;

		// Token: 0x04000FA7 RID: 4007
		public bool? isBanned;

		// Token: 0x04000FA8 RID: 4008
		public DateTime? LastLogin;

		// Token: 0x04000FA9 RID: 4009
		public UserOrigination? Origination;

		// Token: 0x04000FAA RID: 4010
		public EntityKey TitlePlayerAccount;
	}
}
