using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000223 RID: 547
	[Serializable]
	public class GetPlayerTradesResponse : PlayFabResultCommon
	{
		// Token: 0x04000C5A RID: 3162
		public List<TradeInfo> AcceptedTrades;

		// Token: 0x04000C5B RID: 3163
		public List<TradeInfo> OpenedTrades;
	}
}
