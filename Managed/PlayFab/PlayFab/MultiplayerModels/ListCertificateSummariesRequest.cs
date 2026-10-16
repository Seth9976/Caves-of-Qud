using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000C0 RID: 192
	[Serializable]
	public class ListCertificateSummariesRequest : PlayFabRequestCommon
	{
		// Token: 0x040004F5 RID: 1269
		public Dictionary<string, string> CustomTags;

		// Token: 0x040004F6 RID: 1270
		public int? PageSize;

		// Token: 0x040004F7 RID: 1271
		public string SkipToken;
	}
}
