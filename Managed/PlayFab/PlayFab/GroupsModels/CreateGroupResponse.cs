using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000117 RID: 279
	[Serializable]
	public class CreateGroupResponse : PlayFabResultCommon
	{
		// Token: 0x04000613 RID: 1555
		public string AdminRoleId;

		// Token: 0x04000614 RID: 1556
		public DateTime Created;

		// Token: 0x04000615 RID: 1557
		public EntityKey Group;

		// Token: 0x04000616 RID: 1558
		public string GroupName;

		// Token: 0x04000617 RID: 1559
		public string MemberRoleId;

		// Token: 0x04000618 RID: 1560
		public int ProfileVersion;

		// Token: 0x04000619 RID: 1561
		public Dictionary<string, string> Roles;
	}
}
