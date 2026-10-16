using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public class AcceptGroupApplicationRequest : PlayFabRequestCommon
	{
		// Token: 0x040005F7 RID: 1527
		public Dictionary<string, string> CustomTags;

		// Token: 0x040005F8 RID: 1528
		public EntityKey Entity;

		// Token: 0x040005F9 RID: 1529
		public EntityKey Group;
	}
}
