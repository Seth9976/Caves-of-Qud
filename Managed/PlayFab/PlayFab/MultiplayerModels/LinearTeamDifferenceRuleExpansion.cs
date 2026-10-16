using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000B9 RID: 185
	[Serializable]
	public class LinearTeamDifferenceRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x040004E2 RID: 1250
		public double Delta;

		// Token: 0x040004E3 RID: 1251
		public double? Limit;

		// Token: 0x040004E4 RID: 1252
		public uint SecondsBetweenExpansions;
	}
}
