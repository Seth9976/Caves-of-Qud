using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	public class GetMatchmakingTicketResult : PlayFabResultCommon
	{
		// Token: 0x04000494 RID: 1172
		public string CancellationReasonString;

		// Token: 0x04000495 RID: 1173
		public DateTime Created;

		// Token: 0x04000496 RID: 1174
		public EntityKey Creator;

		// Token: 0x04000497 RID: 1175
		public int GiveUpAfterSeconds;

		// Token: 0x04000498 RID: 1176
		public string MatchId;

		// Token: 0x04000499 RID: 1177
		public List<MatchmakingPlayer> Members;

		// Token: 0x0400049A RID: 1178
		public List<EntityKey> MembersToMatchWith;

		// Token: 0x0400049B RID: 1179
		public string QueueName;

		// Token: 0x0400049C RID: 1180
		public string Status;

		// Token: 0x0400049D RID: 1181
		public string TicketId;
	}
}
