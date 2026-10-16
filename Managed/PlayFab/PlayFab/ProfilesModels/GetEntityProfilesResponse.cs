using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public class GetEntityProfilesResponse : PlayFabResultCommon
	{
		// Token: 0x04000321 RID: 801
		public List<EntityProfileBody> Profiles;
	}
}
