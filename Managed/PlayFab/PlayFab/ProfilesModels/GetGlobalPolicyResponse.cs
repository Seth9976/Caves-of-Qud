using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class GetGlobalPolicyResponse : PlayFabResultCommon
	{
		// Token: 0x04000323 RID: 803
		public List<EntityPermissionStatement> Permissions;
	}
}
