using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	public class JoinMatchmakingTicketRequest : PlayFabRequestCommon
	{
		// Token: 0x040004D6 RID: 1238
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004D7 RID: 1239
		public MatchmakingPlayer Member;

		// Token: 0x040004D8 RID: 1240
		public string QueueName;

		// Token: 0x040004D9 RID: 1241
		public string TicketId;
	}
}
