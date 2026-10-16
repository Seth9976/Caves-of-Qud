using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000176 RID: 374
	[Serializable]
	public class AdCampaignAttributionModel : PlayFabBaseModel
	{
		// Token: 0x04000754 RID: 1876
		public DateTime AttributedAt;

		// Token: 0x04000755 RID: 1877
		public string CampaignId;

		// Token: 0x04000756 RID: 1878
		public string Platform;
	}
}
