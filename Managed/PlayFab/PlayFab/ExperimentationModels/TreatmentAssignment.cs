using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	public class TreatmentAssignment : PlayFabBaseModel
	{
		// Token: 0x040006D9 RID: 1753
		public List<Variable> Variables;

		// Token: 0x040006DA RID: 1754
		public List<string> Variants;
	}
}
