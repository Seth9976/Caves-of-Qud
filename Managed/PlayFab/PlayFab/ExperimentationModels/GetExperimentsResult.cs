using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x0200014A RID: 330
	[Serializable]
	public class GetExperimentsResult : PlayFabResultCommon
	{
		// Token: 0x040006B5 RID: 1717
		public List<Experiment> Experiments;
	}
}
