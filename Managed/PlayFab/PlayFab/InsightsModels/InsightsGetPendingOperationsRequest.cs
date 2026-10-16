using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	public class InsightsGetPendingOperationsRequest : PlayFabRequestCommon
	{
		// Token: 0x040005E5 RID: 1509
		public Dictionary<string, string> CustomTags;

		// Token: 0x040005E6 RID: 1510
		public string OperationType;
	}
}
