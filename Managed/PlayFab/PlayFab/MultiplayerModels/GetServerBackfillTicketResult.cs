using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	public class GetServerBackfillTicketResult : PlayFabResultCommon
	{
		// Token: 0x040004C8 RID: 1224
		public string CancellationReasonString;

		// Token: 0x040004C9 RID: 1225
		public DateTime Created;

		// Token: 0x040004CA RID: 1226
		public int GiveUpAfterSeconds;

		// Token: 0x040004CB RID: 1227
		public string MatchId;

		// Token: 0x040004CC RID: 1228
		public List<MatchmakingPlayerWithTeamAssignment> Members;

		// Token: 0x040004CD RID: 1229
		public string QueueName;

		// Token: 0x040004CE RID: 1230
		public ServerDetails ServerDetails;

		// Token: 0x040004CF RID: 1231
		public string Status;

		// Token: 0x040004D0 RID: 1232
		public string TicketId;
	}
}
