using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	public class SetMatchmakingQueueRequest : PlayFabRequestCommon
	{
		// Token: 0x04000593 RID: 1427
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000594 RID: 1428
		public MatchmakingQueueConfig MatchmakingQueue;
	}
}
