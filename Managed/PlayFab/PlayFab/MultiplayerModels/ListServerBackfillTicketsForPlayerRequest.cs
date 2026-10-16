using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000D2 RID: 210
	[Serializable]
	public class ListServerBackfillTicketsForPlayerRequest : PlayFabRequestCommon
	{
		// Token: 0x0400051F RID: 1311
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000520 RID: 1312
		public EntityKey Entity;

		// Token: 0x04000521 RID: 1313
		public string QueueName;
	}
}
