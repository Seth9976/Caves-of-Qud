using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000116 RID: 278
	[Serializable]
	public class CreateGroupRequest : PlayFabRequestCommon
	{
		// Token: 0x04000610 RID: 1552
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000611 RID: 1553
		public EntityKey Entity;

		// Token: 0x04000612 RID: 1554
		public string GroupName;
	}
}
