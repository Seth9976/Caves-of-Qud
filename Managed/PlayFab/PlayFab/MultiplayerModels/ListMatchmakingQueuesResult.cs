using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	public class ListMatchmakingQueuesResult : PlayFabResultCommon
	{
		// Token: 0x04000505 RID: 1285
		public List<MatchmakingQueueConfig> MatchMakingQueues;
	}
}
