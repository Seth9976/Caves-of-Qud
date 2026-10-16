using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public class CreateGroupRoleRequest : PlayFabRequestCommon
	{
		// Token: 0x0400061A RID: 1562
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400061B RID: 1563
		public EntityKey Group;

		// Token: 0x0400061C RID: 1564
		public string RoleId;

		// Token: 0x0400061D RID: 1565
		public string RoleName;
	}
}
