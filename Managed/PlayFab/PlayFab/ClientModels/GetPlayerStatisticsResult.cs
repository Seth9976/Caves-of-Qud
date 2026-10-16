using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200021D RID: 541
	[Serializable]
	public class GetPlayerStatisticsResult : PlayFabResultCommon
	{
		// Token: 0x04000C50 RID: 3152
		public List<StatisticValue> Statistics;
	}
}
