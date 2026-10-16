using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000328 RID: 808
	[Serializable]
	public class ValidateAmazonReceiptRequest : PlayFabRequestCommon
	{
		// Token: 0x04000FB0 RID: 4016
		public string CatalogVersion;

		// Token: 0x04000FB1 RID: 4017
		public string CurrencyCode;

		// Token: 0x04000FB2 RID: 4018
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000FB3 RID: 4019
		public int PurchasePrice;

		// Token: 0x04000FB4 RID: 4020
		public string ReceiptId;

		// Token: 0x04000FB5 RID: 4021
		public string UserId;
	}
}
