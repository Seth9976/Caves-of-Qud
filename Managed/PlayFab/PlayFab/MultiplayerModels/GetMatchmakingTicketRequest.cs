using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	public class GetMatchmakingTicketRequest : PlayFabRequestCommon
	{
		// Token: 0x04000490 RID: 1168
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000491 RID: 1169
		public bool EscapeObject;

		// Token: 0x04000492 RID: 1170
		public string QueueName;

		// Token: 0x04000493 RID: 1171
		public string TicketId;
	}
}
