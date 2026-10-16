using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000308 RID: 776
	[Serializable]
	public class UpdatePlayerStatisticsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F40 RID: 3904
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F41 RID: 3905
		public List<StatisticUpdate> Statistics;
	}
}
