using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public class AcceptGroupInvitationRequest : PlayFabRequestCommon
	{
		// Token: 0x040005FA RID: 1530
		public Dictionary<string, string> CustomTags;

		// Token: 0x040005FB RID: 1531
		public EntityKey Entity;

		// Token: 0x040005FC RID: 1532
		public EntityKey Group;
	}
}
