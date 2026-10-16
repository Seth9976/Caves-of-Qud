using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	public class ListGroupBlocksRequest : PlayFabRequestCommon
	{
		// Token: 0x04000659 RID: 1625
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400065A RID: 1626
		public EntityKey Group;
	}
}
