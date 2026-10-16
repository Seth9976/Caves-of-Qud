using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000134 RID: 308
	[Serializable]
	public class ListMembershipOpportunitiesResponse : PlayFabResultCommon
	{
		// Token: 0x04000664 RID: 1636
		public List<GroupApplication> Applications;

		// Token: 0x04000665 RID: 1637
		public List<GroupInvitation> Invitations;
	}
}
