using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000085 RID: 133
	[Serializable]
	public class CustomTeamDifferenceRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x04000440 RID: 1088
		public List<OverrideDouble> DifferenceOverrides;

		// Token: 0x04000441 RID: 1089
		public uint SecondsBetweenExpansions;
	}
}
