using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	public class GetMatchmakingQueueResult : PlayFabResultCommon
	{
		// Token: 0x0400048F RID: 1167
		public MatchmakingQueueConfig MatchmakingQueue;
	}
}
