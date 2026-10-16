using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002A8 RID: 680
	[Serializable]
	public class RedeemCouponRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E38 RID: 3640
		public string CatalogVersion;

		// Token: 0x04000E39 RID: 3641
		public string CharacterId;

		// Token: 0x04000E3A RID: 3642
		public string CouponCode;

		// Token: 0x04000E3B RID: 3643
		public Dictionary<string, string> CustomTags;
	}
}
