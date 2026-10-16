using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200013C RID: 316
	[Serializable]
	public class UpdateGroupRequest : PlayFabRequestCommon
	{
		// Token: 0x0400067B RID: 1659
		public string AdminRoleId;

		// Token: 0x0400067C RID: 1660
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400067D RID: 1661
		public int? ExpectedProfileVersion;

		// Token: 0x0400067E RID: 1662
		public EntityKey Group;

		// Token: 0x0400067F RID: 1663
		public string GroupName;

		// Token: 0x04000680 RID: 1664
		public string MemberRoleId;
	}
}
