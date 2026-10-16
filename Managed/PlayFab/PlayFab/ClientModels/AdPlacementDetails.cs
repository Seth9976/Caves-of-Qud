using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001BD RID: 445
	[Serializable]
	public class AdPlacementDetails : PlayFabBaseModel
	{
		// Token: 0x04000972 RID: 2418
		public string PlacementId;

		// Token: 0x04000973 RID: 2419
		public string PlacementName;

		// Token: 0x04000974 RID: 2420
		public int? PlacementViewsRemaining;

		// Token: 0x04000975 RID: 2421
		public double? PlacementViewsResetMinutes;

		// Token: 0x04000976 RID: 2422
		public string RewardAssetUrl;

		// Token: 0x04000977 RID: 2423
		public string RewardDescription;

		// Token: 0x04000978 RID: 2424
		public string RewardId;

		// Token: 0x04000979 RID: 2425
		public string RewardName;
	}
}
