using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AuthenticationModels
{
	// Token: 0x0200033B RID: 827
	[Serializable]
	public class GetEntityTokenRequest : PlayFabRequestCommon
	{
		// Token: 0x04001229 RID: 4649
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400122A RID: 4650
		public EntityKey Entity;
	}
}
