using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x0200014B RID: 331
	[Serializable]
	public class GetLatestScorecardRequest : PlayFabRequestCommon
	{
		// Token: 0x040006B6 RID: 1718
		public Dictionary<string, string> CustomTags;

		// Token: 0x040006B7 RID: 1719
		public string ExperimentId;
	}
}
