using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class BuildSelectionCriterion : PlayFabBaseModel
	{
		// Token: 0x04000394 RID: 916
		public Dictionary<string, uint> BuildWeightDistribution;
	}
}
