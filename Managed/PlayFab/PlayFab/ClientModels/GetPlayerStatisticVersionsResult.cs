using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200021F RID: 543
	[Serializable]
	public class GetPlayerStatisticVersionsResult : PlayFabResultCommon
	{
		// Token: 0x04000C53 RID: 3155
		public List<PlayerStatisticVersion> StatisticVersions;
	}
}
