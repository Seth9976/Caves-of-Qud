using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000BB RID: 187
	[Serializable]
	public class ListAssetSummariesRequest : PlayFabRequestCommon
	{
		// Token: 0x040004E8 RID: 1256
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004E9 RID: 1257
		public int? PageSize;

		// Token: 0x040004EA RID: 1258
		public string SkipToken;
	}
}
