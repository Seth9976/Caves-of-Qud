using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class CancelAllServerBackfillTicketsForPlayerRequest : PlayFabRequestCommon
	{
		// Token: 0x0400039D RID: 925
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400039E RID: 926
		public EntityKey Entity;

		// Token: 0x0400039F RID: 927
		public string QueueName;
	}
}
