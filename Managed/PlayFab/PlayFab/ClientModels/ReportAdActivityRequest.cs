using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002BA RID: 698
	[Serializable]
	public class ReportAdActivityRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E6B RID: 3691
		public AdActivity Activity;

		// Token: 0x04000E6C RID: 3692
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000E6D RID: 3693
		public string PlacementId;

		// Token: 0x04000E6E RID: 3694
		public string RewardId;
	}
}
