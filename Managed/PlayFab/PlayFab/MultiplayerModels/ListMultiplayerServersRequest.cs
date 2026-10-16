using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	public class ListMultiplayerServersRequest : PlayFabRequestCommon
	{
		// Token: 0x0400050A RID: 1290
		public string BuildId;

		// Token: 0x0400050B RID: 1291
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400050C RID: 1292
		public int? PageSize;

		// Token: 0x0400050D RID: 1293
		public string Region;

		// Token: 0x0400050E RID: 1294
		public string SkipToken;
	}
}
