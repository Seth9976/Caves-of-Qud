using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000207 RID: 519
	[Serializable]
	public class GetFriendsListResult : PlayFabResultCommon
	{
		// Token: 0x04000C07 RID: 3079
		public List<FriendInfo> Friends;
	}
}
