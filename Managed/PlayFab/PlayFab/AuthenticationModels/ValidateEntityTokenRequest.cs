using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.AuthenticationModels
{
	// Token: 0x0200033E RID: 830
	[Serializable]
	public class ValidateEntityTokenRequest : PlayFabRequestCommon
	{
		// Token: 0x04001244 RID: 4676
		public Dictionary<string, string> CustomTags;

		// Token: 0x04001245 RID: 4677
		public string EntityToken;
	}
}
