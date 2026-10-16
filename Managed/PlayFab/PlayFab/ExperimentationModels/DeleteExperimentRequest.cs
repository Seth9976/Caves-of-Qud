using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000143 RID: 323
	[Serializable]
	public class DeleteExperimentRequest : PlayFabRequestCommon
	{
		// Token: 0x0400069E RID: 1694
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400069F RID: 1695
		public string ExperimentId;
	}
}
