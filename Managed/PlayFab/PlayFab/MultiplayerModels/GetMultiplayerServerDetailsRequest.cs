using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000A4 RID: 164
	[Serializable]
	public class GetMultiplayerServerDetailsRequest : PlayFabRequestCommon
	{
		// Token: 0x040004A7 RID: 1191
		public string BuildId;

		// Token: 0x040004A8 RID: 1192
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004A9 RID: 1193
		public string Region;

		// Token: 0x040004AA RID: 1194
		public string SessionId;
	}
}
