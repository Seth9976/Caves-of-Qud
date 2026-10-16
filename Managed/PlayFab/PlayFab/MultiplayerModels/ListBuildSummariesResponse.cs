using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	public class ListBuildSummariesResponse : PlayFabResultCommon
	{
		// Token: 0x040004F2 RID: 1266
		public List<BuildSummary> BuildSummaries;

		// Token: 0x040004F3 RID: 1267
		public int PageSize;

		// Token: 0x040004F4 RID: 1268
		public string SkipToken;
	}
}
