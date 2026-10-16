using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000A8 RID: 168
	[Serializable]
	public class GetMultiplayerSessionLogsBySessionIdRequest : PlayFabRequestCommon
	{
		// Token: 0x040004B8 RID: 1208
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004B9 RID: 1209
		public string SessionId;
	}
}
