using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	public class ListMembershipResponse : PlayFabResultCommon
	{
		// Token: 0x04000668 RID: 1640
		public List<GroupWithRoles> Groups;
	}
}
