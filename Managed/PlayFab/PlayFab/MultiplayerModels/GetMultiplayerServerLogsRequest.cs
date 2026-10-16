using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public class GetMultiplayerServerLogsRequest : PlayFabRequestCommon
	{
		// Token: 0x040004B5 RID: 1205
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004B6 RID: 1206
		public string ServerId;
	}
}
