using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	public class AddFriendRequest : PlayFabRequestCommon
	{
		// Token: 0x04000960 RID: 2400
		public string FriendEmail;

		// Token: 0x04000961 RID: 2401
		public string FriendPlayFabId;

		// Token: 0x04000962 RID: 2402
		public string FriendTitleDisplayName;

		// Token: 0x04000963 RID: 2403
		public string FriendUsername;
	}
}
