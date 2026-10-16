using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000135 RID: 309
	[Serializable]
	public class ListMembershipRequest : PlayFabRequestCommon
	{
		// Token: 0x04000666 RID: 1638
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000667 RID: 1639
		public EntityKey Entity;
	}
}
