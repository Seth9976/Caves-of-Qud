using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	public class CancelMatchmakingTicketRequest : PlayFabRequestCommon
	{
		// Token: 0x040003A4 RID: 932
		public Dictionary<string, string> CustomTags;

		// Token: 0x040003A5 RID: 933
		public string QueueName;

		// Token: 0x040003A6 RID: 934
		public string TicketId;
	}
}
