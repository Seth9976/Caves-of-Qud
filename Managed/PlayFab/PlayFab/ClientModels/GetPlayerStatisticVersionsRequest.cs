using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200021E RID: 542
	[Serializable]
	public class GetPlayerStatisticVersionsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C51 RID: 3153
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000C52 RID: 3154
		public string StatisticName;
	}
}
