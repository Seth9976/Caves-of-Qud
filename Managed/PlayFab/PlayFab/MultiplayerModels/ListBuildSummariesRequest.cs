using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000BE RID: 190
	[Serializable]
	public class ListBuildSummariesRequest : PlayFabRequestCommon
	{
		// Token: 0x040004EF RID: 1263
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004F0 RID: 1264
		public int? PageSize;

		// Token: 0x040004F1 RID: 1265
		public string SkipToken;
	}
}
