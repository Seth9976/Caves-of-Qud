using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	public class SetEntityProfilePolicyRequest : PlayFabRequestCommon
	{
		// Token: 0x0400032E RID: 814
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400032F RID: 815
		public EntityKey Entity;

		// Token: 0x04000330 RID: 816
		public List<EntityPermissionStatement> Statements;
	}
}
