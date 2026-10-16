using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001D6 RID: 470
	[Serializable]
	public class ConsumeXboxEntitlementsRequest : PlayFabRequestCommon
	{
		// Token: 0x040009CD RID: 2509
		public string CatalogVersion;

		// Token: 0x040009CE RID: 2510
		public Dictionary<string, string> CustomTags;

		// Token: 0x040009CF RID: 2511
		public string XboxToken;
	}
}
