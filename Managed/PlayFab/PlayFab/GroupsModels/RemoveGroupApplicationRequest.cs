using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000138 RID: 312
	[Serializable]
	public class RemoveGroupApplicationRequest : PlayFabRequestCommon
	{
		// Token: 0x0400066E RID: 1646
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400066F RID: 1647
		public EntityKey Entity;

		// Token: 0x04000670 RID: 1648
		public EntityKey Group;
	}
}
