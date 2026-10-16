using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public class ListGroupMembersResponse : PlayFabResultCommon
	{
		// Token: 0x04000661 RID: 1633
		public List<EntityMemberRole> Members;
	}
}
