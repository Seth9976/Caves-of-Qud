using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000123 RID: 291
	[Serializable]
	public class GroupBlock : PlayFabBaseModel
	{
		// Token: 0x0400063A RID: 1594
		public EntityWithLineage Entity;

		// Token: 0x0400063B RID: 1595
		public EntityKey Group;
	}
}
