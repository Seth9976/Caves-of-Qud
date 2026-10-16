using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000BC RID: 188
	[Serializable]
	public class ListAssetSummariesResponse : PlayFabResultCommon
	{
		// Token: 0x040004EB RID: 1259
		public List<AssetSummary> AssetSummaries;

		// Token: 0x040004EC RID: 1260
		public int PageSize;

		// Token: 0x040004ED RID: 1261
		public string SkipToken;
	}
}
