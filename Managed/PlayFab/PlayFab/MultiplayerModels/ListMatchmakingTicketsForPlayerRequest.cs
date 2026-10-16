using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000C8 RID: 200
	[Serializable]
	public class ListMatchmakingTicketsForPlayerRequest : PlayFabRequestCommon
	{
		// Token: 0x04000506 RID: 1286
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000507 RID: 1287
		public EntityKey Entity;

		// Token: 0x04000508 RID: 1288
		public string QueueName;
	}
}
