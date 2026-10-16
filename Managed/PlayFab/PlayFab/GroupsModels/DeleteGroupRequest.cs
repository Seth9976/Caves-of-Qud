using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200011A RID: 282
	[Serializable]
	public class DeleteGroupRequest : PlayFabRequestCommon
	{
		// Token: 0x04000621 RID: 1569
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000622 RID: 1570
		public EntityKey Group;
	}
}
