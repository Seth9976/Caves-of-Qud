using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002C1 RID: 705
	[Serializable]
	public class RewardAdActivityResult : PlayFabResultCommon
	{
		// Token: 0x04000E7A RID: 3706
		public string AdActivityEventId;

		// Token: 0x04000E7B RID: 3707
		public List<string> DebugResults;

		// Token: 0x04000E7C RID: 3708
		public string PlacementId;

		// Token: 0x04000E7D RID: 3709
		public string PlacementName;

		// Token: 0x04000E7E RID: 3710
		public int? PlacementViewsRemaining;

		// Token: 0x04000E7F RID: 3711
		public double? PlacementViewsResetMinutes;

		// Token: 0x04000E80 RID: 3712
		public AdRewardResults RewardResults;
	}
}
