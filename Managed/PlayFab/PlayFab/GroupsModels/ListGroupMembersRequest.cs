using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000131 RID: 305
	[Serializable]
	public class ListGroupMembersRequest : PlayFabRequestCommon
	{
		// Token: 0x0400065F RID: 1631
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000660 RID: 1632
		public EntityKey Group;
	}
}
