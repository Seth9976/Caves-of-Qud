using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public class GetEntityProfilesRequest : PlayFabRequestCommon
	{
		// Token: 0x0400031E RID: 798
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400031F RID: 799
		public bool? DataAsObject;

		// Token: 0x04000320 RID: 800
		public List<EntityKey> Entities;
	}
}
