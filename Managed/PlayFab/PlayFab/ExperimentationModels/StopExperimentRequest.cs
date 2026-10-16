using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000153 RID: 339
	[Serializable]
	public class StopExperimentRequest : PlayFabRequestCommon
	{
		// Token: 0x040006D7 RID: 1751
		public Dictionary<string, string> CustomTags;

		// Token: 0x040006D8 RID: 1752
		public string ExperimentId;
	}
}
