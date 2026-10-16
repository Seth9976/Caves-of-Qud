using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000E7 RID: 231
	[Serializable]
	public class RemoveMatchmakingQueueRequest : PlayFabRequestCommon
	{
		// Token: 0x0400056D RID: 1389
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400056E RID: 1390
		public string QueueName;
	}
}
