using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public class MatchTotalRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x0400054C RID: 1356
		public List<OverrideDouble> MaxOverrides;

		// Token: 0x0400054D RID: 1357
		public List<OverrideDouble> MinOverrides;

		// Token: 0x0400054E RID: 1358
		public uint SecondsBetweenExpansions;
	}
}
