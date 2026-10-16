using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public class SetGlobalPolicyRequest : PlayFabRequestCommon
	{
		// Token: 0x04000332 RID: 818
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000333 RID: 819
		public List<EntityPermissionStatement> Permissions;
	}
}
