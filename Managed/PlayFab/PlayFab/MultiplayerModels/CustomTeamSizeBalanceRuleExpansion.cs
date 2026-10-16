using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000086 RID: 134
	[Serializable]
	public class CustomTeamSizeBalanceRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x04000442 RID: 1090
		public List<OverrideUnsignedInt> DifferenceOverrides;

		// Token: 0x04000443 RID: 1091
		public uint SecondsBetweenExpansions;
	}
}
