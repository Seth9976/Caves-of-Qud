using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public class AddMembersRequest : PlayFabRequestCommon
	{
		// Token: 0x040005FD RID: 1533
		public Dictionary<string, string> CustomTags;

		// Token: 0x040005FE RID: 1534
		public EntityKey Group;

		// Token: 0x040005FF RID: 1535
		public List<EntityKey> Members;

		// Token: 0x04000600 RID: 1536
		public string RoleId;
	}
}
