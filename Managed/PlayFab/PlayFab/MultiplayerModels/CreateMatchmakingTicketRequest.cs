using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200007A RID: 122
	[Serializable]
	public class CreateMatchmakingTicketRequest : PlayFabRequestCommon
	{
		// Token: 0x0400041D RID: 1053
		public MatchmakingPlayer Creator;

		// Token: 0x0400041E RID: 1054
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400041F RID: 1055
		public int GiveUpAfterSeconds;

		// Token: 0x04000420 RID: 1056
		public List<EntityKey> MembersToMatchWith;

		// Token: 0x04000421 RID: 1057
		public string QueueName;
	}
}
