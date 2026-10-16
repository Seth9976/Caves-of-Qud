using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000152 RID: 338
	[Serializable]
	public class StartExperimentRequest : PlayFabRequestCommon
	{
		// Token: 0x040006D5 RID: 1749
		public Dictionary<string, string> CustomTags;

		// Token: 0x040006D6 RID: 1750
		public string ExperimentId;
	}
}
