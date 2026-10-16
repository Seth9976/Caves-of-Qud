using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200011B RID: 283
	[Serializable]
	public class DeleteRoleRequest : PlayFabRequestCommon
	{
		// Token: 0x04000623 RID: 1571
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000624 RID: 1572
		public EntityKey Group;

		// Token: 0x04000625 RID: 1573
		public string RoleId;
	}
}
