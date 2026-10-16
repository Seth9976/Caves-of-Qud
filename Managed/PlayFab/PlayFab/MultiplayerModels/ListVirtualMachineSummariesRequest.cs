using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000D4 RID: 212
	[Serializable]
	public class ListVirtualMachineSummariesRequest : PlayFabRequestCommon
	{
		// Token: 0x04000523 RID: 1315
		public string BuildId;

		// Token: 0x04000524 RID: 1316
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000525 RID: 1317
		public int? PageSize;

		// Token: 0x04000526 RID: 1318
		public string Region;

		// Token: 0x04000527 RID: 1319
		public string SkipToken;
	}
}
