using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public class ShutdownMultiplayerServerRequest : PlayFabRequestCommon
	{
		// Token: 0x04000595 RID: 1429
		public string BuildId;

		// Token: 0x04000596 RID: 1430
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000597 RID: 1431
		public string Region;

		// Token: 0x04000598 RID: 1432
		public string SessionId;
	}
}
