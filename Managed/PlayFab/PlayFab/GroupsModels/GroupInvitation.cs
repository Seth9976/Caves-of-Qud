using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000124 RID: 292
	[Serializable]
	public class GroupInvitation : PlayFabBaseModel
	{
		// Token: 0x0400063C RID: 1596
		public DateTime Expires;

		// Token: 0x0400063D RID: 1597
		public EntityKey Group;

		// Token: 0x0400063E RID: 1598
		public EntityWithLineage InvitedByEntity;

		// Token: 0x0400063F RID: 1599
		public EntityWithLineage InvitedEntity;

		// Token: 0x04000640 RID: 1600
		public string RoleId;
	}
}
