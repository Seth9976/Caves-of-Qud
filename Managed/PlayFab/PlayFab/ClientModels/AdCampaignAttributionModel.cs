using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001B1 RID: 433
	[Serializable]
	public class AdCampaignAttributionModel : PlayFabBaseModel
	{
		// Token: 0x0400095D RID: 2397
		public DateTime AttributedAt;

		// Token: 0x0400095E RID: 2398
		public string CampaignId;

		// Token: 0x0400095F RID: 2399
		public string Platform;
	}
}
