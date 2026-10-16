using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class GetEntityProfileRequest : PlayFabRequestCommon
	{
		// Token: 0x0400031A RID: 794
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400031B RID: 795
		public bool? DataAsObject;

		// Token: 0x0400031C RID: 796
		public EntityKey Entity;
	}
}
