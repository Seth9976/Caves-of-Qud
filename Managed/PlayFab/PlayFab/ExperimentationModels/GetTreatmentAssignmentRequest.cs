using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	public class GetTreatmentAssignmentRequest : PlayFabRequestCommon
	{
		// Token: 0x040006B9 RID: 1721
		public Dictionary<string, string> CustomTags;

		// Token: 0x040006BA RID: 1722
		public EntityKey Entity;
	}
}
