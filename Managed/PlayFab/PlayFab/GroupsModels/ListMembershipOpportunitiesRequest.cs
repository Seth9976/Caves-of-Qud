using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000133 RID: 307
	[Serializable]
	public class ListMembershipOpportunitiesRequest : PlayFabRequestCommon
	{
		// Token: 0x04000662 RID: 1634
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000663 RID: 1635
		public EntityKey Entity;
	}
}
