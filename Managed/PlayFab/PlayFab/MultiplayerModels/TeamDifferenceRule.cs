using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public class TeamDifferenceRule : PlayFabBaseModel
	{
		// Token: 0x040005A8 RID: 1448
		public QueueRuleAttribute Attribute;

		// Token: 0x040005A9 RID: 1449
		public CustomTeamDifferenceRuleExpansion CustomExpansion;

		// Token: 0x040005AA RID: 1450
		public double DefaultAttributeValue;

		// Token: 0x040005AB RID: 1451
		public double Difference;

		// Token: 0x040005AC RID: 1452
		public LinearTeamDifferenceRuleExpansion LinearExpansion;

		// Token: 0x040005AD RID: 1453
		public string Name;

		// Token: 0x040005AE RID: 1454
		public uint? SecondsUntilOptional;
	}
}
