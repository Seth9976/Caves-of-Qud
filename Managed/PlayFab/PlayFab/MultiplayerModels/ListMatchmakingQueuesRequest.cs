using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000C6 RID: 198
	[Serializable]
	public class ListMatchmakingQueuesRequest : PlayFabRequestCommon
	{
		// Token: 0x04000504 RID: 1284
		public Dictionary<string, string> CustomTags;
	}
}
