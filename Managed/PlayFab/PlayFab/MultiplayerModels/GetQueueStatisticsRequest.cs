using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000A9 RID: 169
	[Serializable]
	public class GetQueueStatisticsRequest : PlayFabRequestCommon
	{
		// Token: 0x040004BA RID: 1210
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004BB RID: 1211
		public string QueueName;
	}
}
