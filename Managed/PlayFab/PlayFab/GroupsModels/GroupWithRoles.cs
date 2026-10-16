using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000126 RID: 294
	[Serializable]
	public class GroupWithRoles : PlayFabBaseModel
	{
		// Token: 0x04000643 RID: 1603
		public EntityKey Group;

		// Token: 0x04000644 RID: 1604
		public string GroupName;

		// Token: 0x04000645 RID: 1605
		public int ProfileVersion;

		// Token: 0x04000646 RID: 1606
		public List<GroupRole> Roles;
	}
}
