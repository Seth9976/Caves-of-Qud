using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000BA RID: 186
	[Serializable]
	public class LinearTeamSizeBalanceRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x040004E5 RID: 1253
		public uint Delta;

		// Token: 0x040004E6 RID: 1254
		public uint? Limit;

		// Token: 0x040004E7 RID: 1255
		public uint SecondsBetweenExpansions;
	}
}
