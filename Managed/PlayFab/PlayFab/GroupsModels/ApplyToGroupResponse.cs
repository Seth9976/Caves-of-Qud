using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public class ApplyToGroupResponse : PlayFabResultCommon
	{
		// Token: 0x04000605 RID: 1541
		public EntityWithLineage Entity;

		// Token: 0x04000606 RID: 1542
		public DateTime Expires;

		// Token: 0x04000607 RID: 1543
		public EntityKey Group;
	}
}
