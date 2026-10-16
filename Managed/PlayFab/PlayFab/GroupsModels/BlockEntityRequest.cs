using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public class BlockEntityRequest : PlayFabRequestCommon
	{
		// Token: 0x04000608 RID: 1544
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000609 RID: 1545
		public EntityKey Entity;

		// Token: 0x0400060A RID: 1546
		public EntityKey Group;
	}
}
