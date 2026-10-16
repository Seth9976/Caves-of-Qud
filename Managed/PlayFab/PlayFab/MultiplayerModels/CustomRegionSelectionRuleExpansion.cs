using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000083 RID: 131
	[Serializable]
	public class CustomRegionSelectionRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x0400043C RID: 1084
		public List<OverrideUnsignedInt> MaxLatencyOverrides;

		// Token: 0x0400043D RID: 1085
		public uint SecondsBetweenExpansions;
	}
}
