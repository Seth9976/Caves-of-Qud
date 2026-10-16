using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	public class AcceptTradeRequest : PlayFabRequestCommon
	{
		// Token: 0x04000954 RID: 2388
		public List<string> AcceptedInventoryInstanceIds;

		// Token: 0x04000955 RID: 2389
		public string OfferingPlayerId;

		// Token: 0x04000956 RID: 2390
		public string TradeId;
	}
}
