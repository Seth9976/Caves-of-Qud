using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public class InsightsGetOperationStatusRequest : PlayFabRequestCommon
	{
		// Token: 0x040005DB RID: 1499
		public Dictionary<string, string> CustomTags;

		// Token: 0x040005DC RID: 1500
		public string OperationId;
	}
}
