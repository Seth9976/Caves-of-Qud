using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public class RequestMultiplayerServerRequest : PlayFabRequestCommon
	{
		// Token: 0x0400056F RID: 1391
		public BuildAliasParams BuildAliasParams;

		// Token: 0x04000570 RID: 1392
		public string BuildId;

		// Token: 0x04000571 RID: 1393
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000572 RID: 1394
		public List<string> InitialPlayers;

		// Token: 0x04000573 RID: 1395
		public List<string> PreferredRegions;

		// Token: 0x04000574 RID: 1396
		public string SessionCookie;

		// Token: 0x04000575 RID: 1397
		public string SessionId;
	}
}
