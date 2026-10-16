using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200021C RID: 540
	[Serializable]
	public class GetPlayerStatisticsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C4D RID: 3149
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000C4E RID: 3150
		public List<string> StatisticNames;

		// Token: 0x04000C4F RID: 3151
		public List<StatisticNameVersion> StatisticNameVersions;
	}
}
