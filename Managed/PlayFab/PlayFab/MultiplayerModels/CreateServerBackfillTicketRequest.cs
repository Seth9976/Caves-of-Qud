using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200007E RID: 126
	[Serializable]
	public class CreateServerBackfillTicketRequest : PlayFabRequestCommon
	{
		// Token: 0x0400042C RID: 1068
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400042D RID: 1069
		public int GiveUpAfterSeconds;

		// Token: 0x0400042E RID: 1070
		public List<MatchmakingPlayerWithTeamAssignment> Members;

		// Token: 0x0400042F RID: 1071
		public string QueueName;

		// Token: 0x04000430 RID: 1072
		public ServerDetails ServerDetails;
	}
}
