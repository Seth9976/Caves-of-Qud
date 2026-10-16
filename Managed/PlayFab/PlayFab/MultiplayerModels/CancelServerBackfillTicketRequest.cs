using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public class CancelServerBackfillTicketRequest : PlayFabRequestCommon
	{
		// Token: 0x040003A7 RID: 935
		public Dictionary<string, string> CustomTags;

		// Token: 0x040003A8 RID: 936
		public string QueueName;

		// Token: 0x040003A9 RID: 937
		public string TicketId;
	}
}
