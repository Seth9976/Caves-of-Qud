using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class CancelAllMatchmakingTicketsForPlayerRequest : PlayFabRequestCommon
	{
		// Token: 0x0400039A RID: 922
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400039B RID: 923
		public EntityKey Entity;

		// Token: 0x0400039C RID: 924
		public string QueueName;
	}
}
