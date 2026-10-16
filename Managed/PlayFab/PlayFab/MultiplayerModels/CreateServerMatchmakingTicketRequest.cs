using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	public class CreateServerMatchmakingTicketRequest : PlayFabRequestCommon
	{
		// Token: 0x04000432 RID: 1074
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000433 RID: 1075
		public int GiveUpAfterSeconds;

		// Token: 0x04000434 RID: 1076
		public List<MatchmakingPlayer> Members;

		// Token: 0x04000435 RID: 1077
		public string QueueName;
	}
}
