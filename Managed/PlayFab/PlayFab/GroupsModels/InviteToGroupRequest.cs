using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000127 RID: 295
	[Serializable]
	public class InviteToGroupRequest : PlayFabRequestCommon
	{
		// Token: 0x04000647 RID: 1607
		public bool? AutoAcceptOutstandingApplication;

		// Token: 0x04000648 RID: 1608
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000649 RID: 1609
		public EntityKey Entity;

		// Token: 0x0400064A RID: 1610
		public EntityKey Group;

		// Token: 0x0400064B RID: 1611
		public string RoleId;
	}
}
