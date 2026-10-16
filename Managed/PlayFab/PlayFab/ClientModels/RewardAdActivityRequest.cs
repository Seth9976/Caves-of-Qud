using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002C0 RID: 704
	[Serializable]
	public class RewardAdActivityRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E77 RID: 3703
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000E78 RID: 3704
		public string PlacementId;

		// Token: 0x04000E79 RID: 3705
		public string RewardId;
	}
}
