using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000139 RID: 313
	[Serializable]
	public class RemoveGroupInvitationRequest : PlayFabRequestCommon
	{
		// Token: 0x04000671 RID: 1649
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000672 RID: 1650
		public EntityKey Entity;

		// Token: 0x04000673 RID: 1651
		public EntityKey Group;
	}
}
