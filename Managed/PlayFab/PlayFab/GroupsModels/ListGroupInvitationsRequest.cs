using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public class ListGroupInvitationsRequest : PlayFabRequestCommon
	{
		// Token: 0x0400065C RID: 1628
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400065D RID: 1629
		public EntityKey Group;
	}
}
