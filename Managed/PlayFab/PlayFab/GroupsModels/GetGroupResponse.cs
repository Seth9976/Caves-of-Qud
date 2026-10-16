using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000121 RID: 289
	[Serializable]
	public class GetGroupResponse : PlayFabResultCommon
	{
		// Token: 0x04000630 RID: 1584
		public string AdminRoleId;

		// Token: 0x04000631 RID: 1585
		public DateTime Created;

		// Token: 0x04000632 RID: 1586
		public EntityKey Group;

		// Token: 0x04000633 RID: 1587
		public string GroupName;

		// Token: 0x04000634 RID: 1588
		public string MemberRoleId;

		// Token: 0x04000635 RID: 1589
		public int ProfileVersion;

		// Token: 0x04000636 RID: 1590
		public Dictionary<string, string> Roles;
	}
}
