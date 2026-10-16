using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	public class AddSharedGroupMembersRequest : PlayFabRequestCommon
	{
		// Token: 0x04000968 RID: 2408
		public List<string> PlayFabIds;

		// Token: 0x04000969 RID: 2409
		public string SharedGroupId;
	}
}
