using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200012B RID: 299
	[Serializable]
	public class ListGroupApplicationsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000656 RID: 1622
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000657 RID: 1623
		public EntityKey Group;
	}
}
