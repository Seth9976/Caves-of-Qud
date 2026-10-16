using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public class GetMatchmakingQueueRequest : PlayFabRequestCommon
	{
		// Token: 0x0400048D RID: 1165
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400048E RID: 1166
		public string QueueName;
	}
}
