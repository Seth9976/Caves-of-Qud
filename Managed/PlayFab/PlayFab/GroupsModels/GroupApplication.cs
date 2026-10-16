using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000122 RID: 290
	[Serializable]
	public class GroupApplication : PlayFabBaseModel
	{
		// Token: 0x04000637 RID: 1591
		public EntityWithLineage Entity;

		// Token: 0x04000638 RID: 1592
		public DateTime Expires;

		// Token: 0x04000639 RID: 1593
		public EntityKey Group;
	}
}
