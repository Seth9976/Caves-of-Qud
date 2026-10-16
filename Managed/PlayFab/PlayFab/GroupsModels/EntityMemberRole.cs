using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200011E RID: 286
	[Serializable]
	public class EntityMemberRole : PlayFabBaseModel
	{
		// Token: 0x04000628 RID: 1576
		public List<EntityWithLineage> Members;

		// Token: 0x04000629 RID: 1577
		public string RoleId;

		// Token: 0x0400062A RID: 1578
		public string RoleName;
	}
}
