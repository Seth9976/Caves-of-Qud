using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public class SetEntityProfilePolicyResponse : PlayFabResultCommon
	{
		// Token: 0x04000331 RID: 817
		public List<EntityPermissionStatement> Permissions;
	}
}
