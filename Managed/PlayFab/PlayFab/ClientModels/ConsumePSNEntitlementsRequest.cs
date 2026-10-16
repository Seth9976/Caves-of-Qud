using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001D4 RID: 468
	[Serializable]
	public class ConsumePSNEntitlementsRequest : PlayFabRequestCommon
	{
		// Token: 0x040009C9 RID: 2505
		public string CatalogVersion;

		// Token: 0x040009CA RID: 2506
		public Dictionary<string, string> CustomTags;

		// Token: 0x040009CB RID: 2507
		public int ServiceLabel;
	}
}
