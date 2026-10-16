using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	public class GetServerBackfillTicketRequest : PlayFabRequestCommon
	{
		// Token: 0x040004C4 RID: 1220
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004C5 RID: 1221
		public bool EscapeObject;

		// Token: 0x040004C6 RID: 1222
		public string QueueName;

		// Token: 0x040004C7 RID: 1223
		public string TicketId;
	}
}
