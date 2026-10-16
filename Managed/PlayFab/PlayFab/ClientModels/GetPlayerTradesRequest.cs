using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000222 RID: 546
	[Serializable]
	public class GetPlayerTradesRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C59 RID: 3161
		public TradeStatus? StatusFilter;
	}
}
