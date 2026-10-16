using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000AA RID: 170
	[Serializable]
	public class GetQueueStatisticsResult : PlayFabResultCommon
	{
		// Token: 0x040004BC RID: 1212
		public uint? NumberOfPlayersMatching;

		// Token: 0x040004BD RID: 1213
		public Statistics TimeToMatchStatisticsInSeconds;
	}
}
