using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	public class ListGroupInvitationsResponse : PlayFabResultCommon
	{
		// Token: 0x0400065E RID: 1630
		public List<GroupInvitation> Invitations;
	}
}
