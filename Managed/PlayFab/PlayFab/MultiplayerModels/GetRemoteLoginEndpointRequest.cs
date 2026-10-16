using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	public class GetRemoteLoginEndpointRequest : PlayFabRequestCommon
	{
		// Token: 0x040004BE RID: 1214
		public string BuildId;

		// Token: 0x040004BF RID: 1215
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004C0 RID: 1216
		public string Region;

		// Token: 0x040004C1 RID: 1217
		public string VmId;
	}
}
