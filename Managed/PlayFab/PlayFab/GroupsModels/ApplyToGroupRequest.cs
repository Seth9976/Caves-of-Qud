using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	public class ApplyToGroupRequest : PlayFabRequestCommon
	{
		// Token: 0x04000601 RID: 1537
		public bool? AutoAcceptOutstandingInvite;

		// Token: 0x04000602 RID: 1538
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000603 RID: 1539
		public EntityKey Entity;

		// Token: 0x04000604 RID: 1540
		public EntityKey Group;
	}
}
