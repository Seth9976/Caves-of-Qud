using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002C5 RID: 709
	[Serializable]
	public class SetFriendTagsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E88 RID: 3720
		public string FriendPlayFabId;

		// Token: 0x04000E89 RID: 3721
		public List<string> Tags;
	}
}
