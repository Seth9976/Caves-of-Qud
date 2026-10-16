using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000082 RID: 130
	[Serializable]
	public class CustomDifferenceRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x0400043A RID: 1082
		public List<OverrideDouble> DifferenceOverrides;

		// Token: 0x0400043B RID: 1083
		public uint SecondsBetweenExpansions;
	}
}
