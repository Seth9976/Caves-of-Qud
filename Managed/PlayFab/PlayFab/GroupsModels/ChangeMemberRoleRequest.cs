using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public class ChangeMemberRoleRequest : PlayFabRequestCommon
	{
		// Token: 0x0400060B RID: 1547
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400060C RID: 1548
		public string DestinationRoleId;

		// Token: 0x0400060D RID: 1549
		public EntityKey Group;

		// Token: 0x0400060E RID: 1550
		public List<EntityKey> Members;

		// Token: 0x0400060F RID: 1551
		public string OriginRoleId;
	}
}
