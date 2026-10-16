using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	public class GetGroupRequest : PlayFabRequestCommon
	{
		// Token: 0x0400062D RID: 1581
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400062E RID: 1582
		public EntityKey Group;

		// Token: 0x0400062F RID: 1583
		public string GroupName;
	}
}
