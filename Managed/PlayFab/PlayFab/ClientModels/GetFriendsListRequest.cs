using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000206 RID: 518
	[Serializable]
	public class GetFriendsListRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C02 RID: 3074
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000C03 RID: 3075
		public bool? IncludeFacebookFriends;

		// Token: 0x04000C04 RID: 3076
		public bool? IncludeSteamFriends;

		// Token: 0x04000C05 RID: 3077
		public PlayerProfileViewConstraints ProfileConstraints;

		// Token: 0x04000C06 RID: 3078
		public string XboxToken;
	}
}
