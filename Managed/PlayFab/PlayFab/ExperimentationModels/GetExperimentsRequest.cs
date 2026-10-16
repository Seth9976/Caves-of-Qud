using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000149 RID: 329
	[Serializable]
	public class GetExperimentsRequest : PlayFabRequestCommon
	{
		// Token: 0x040006B4 RID: 1716
		public Dictionary<string, string> CustomTags;
	}
}
