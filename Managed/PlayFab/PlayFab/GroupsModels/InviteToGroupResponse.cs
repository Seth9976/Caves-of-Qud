using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	public class InviteToGroupResponse : PlayFabResultCommon
	{
		// Token: 0x0400064C RID: 1612
		public DateTime Expires;

		// Token: 0x0400064D RID: 1613
		public EntityKey Group;

		// Token: 0x0400064E RID: 1614
		public EntityWithLineage InvitedByEntity;

		// Token: 0x0400064F RID: 1615
		public EntityWithLineage InvitedEntity;

		// Token: 0x04000650 RID: 1616
		public string RoleId;
	}
}
