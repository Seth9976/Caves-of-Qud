using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000299 RID: 665
	[Serializable]
	public class OpenTradeRequest : PlayFabRequestCommon
	{
		// Token: 0x04000DDE RID: 3550
		public List<string> AllowedPlayerIds;

		// Token: 0x04000DDF RID: 3551
		public List<string> OfferedInventoryInstanceIds;

		// Token: 0x04000DE0 RID: 3552
		public List<string> RequestedCatalogItemIds;
	}
}
