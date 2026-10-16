using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000084 RID: 132
	[Serializable]
	public class CustomSetIntersectionRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x0400043E RID: 1086
		public List<OverrideUnsignedInt> MinIntersectionSizeOverrides;

		// Token: 0x0400043F RID: 1087
		public uint SecondsBetweenExpansions;
	}
}
