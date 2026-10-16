using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200013A RID: 314
	[Serializable]
	public class RemoveMembersRequest : PlayFabRequestCommon
	{
		// Token: 0x04000674 RID: 1652
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000675 RID: 1653
		public EntityKey Group;

		// Token: 0x04000676 RID: 1654
		public List<EntityKey> Members;

		// Token: 0x04000677 RID: 1655
		public string RoleId;
	}
}
