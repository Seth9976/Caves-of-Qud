using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002DF RID: 735
	[Serializable]
	public class TreatmentAssignment : PlayFabBaseModel
	{
		// Token: 0x04000F0C RID: 3852
		public List<Variable> Variables;

		// Token: 0x04000F0D RID: 3853
		public List<string> Variants;
	}
}
