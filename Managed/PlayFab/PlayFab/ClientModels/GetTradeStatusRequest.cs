using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200024B RID: 587
	[Serializable]
	public class GetTradeStatusRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C94 RID: 3220
		public string OfferingPlayerId;

		// Token: 0x04000C95 RID: 3221
		public string TradeId;
	}
}
