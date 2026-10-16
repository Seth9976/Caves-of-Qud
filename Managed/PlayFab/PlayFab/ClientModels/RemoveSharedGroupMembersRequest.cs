using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002B8 RID: 696
	[Serializable]
	public class RemoveSharedGroupMembersRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E69 RID: 3689
		public List<string> PlayFabIds;

		// Token: 0x04000E6A RID: 3690
		public string SharedGroupId;
	}
}
