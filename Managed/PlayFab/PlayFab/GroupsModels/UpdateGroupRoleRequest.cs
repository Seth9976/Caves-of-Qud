using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200013E RID: 318
	[Serializable]
	public class UpdateGroupRoleRequest : PlayFabRequestCommon
	{
		// Token: 0x04000684 RID: 1668
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000685 RID: 1669
		public int? ExpectedProfileVersion;

		// Token: 0x04000686 RID: 1670
		public EntityKey Group;

		// Token: 0x04000687 RID: 1671
		public string RoleId;

		// Token: 0x04000688 RID: 1672
		public string RoleName;
	}
}
