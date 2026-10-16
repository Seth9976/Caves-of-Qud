using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	public class IsMemberRequest : PlayFabRequestCommon
	{
		// Token: 0x04000651 RID: 1617
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000652 RID: 1618
		public EntityKey Entity;

		// Token: 0x04000653 RID: 1619
		public EntityKey Group;

		// Token: 0x04000654 RID: 1620
		public string RoleId;
	}
}
