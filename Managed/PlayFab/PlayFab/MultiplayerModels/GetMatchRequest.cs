using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000A2 RID: 162
	[Serializable]
	public class GetMatchRequest : PlayFabRequestCommon
	{
		// Token: 0x0400049E RID: 1182
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400049F RID: 1183
		public bool EscapeObject;

		// Token: 0x040004A0 RID: 1184
		public string MatchId;

		// Token: 0x040004A1 RID: 1185
		public string QueueName;

		// Token: 0x040004A2 RID: 1186
		public bool ReturnMemberAttributes;
	}
}
