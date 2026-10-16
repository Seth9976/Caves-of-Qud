using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public class TeamSizeBalanceRule : PlayFabBaseModel
	{
		// Token: 0x040005AF RID: 1455
		public CustomTeamSizeBalanceRuleExpansion CustomExpansion;

		// Token: 0x040005B0 RID: 1456
		public uint Difference;

		// Token: 0x040005B1 RID: 1457
		public LinearTeamSizeBalanceRuleExpansion LinearExpansion;

		// Token: 0x040005B2 RID: 1458
		public string Name;

		// Token: 0x040005B3 RID: 1459
		public uint? SecondsUntilOptional;
	}
}
