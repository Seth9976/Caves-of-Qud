using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.InsightsModels
{
	// Token: 0x02000104 RID: 260
	[Serializable]
	public class InsightsEmptyRequest : PlayFabRequestCommon
	{
		// Token: 0x040005CF RID: 1487
		public Dictionary<string, string> CustomTags;
	}
}
