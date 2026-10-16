using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200032A RID: 810
	[Serializable]
	public class ValidateGooglePlayPurchaseRequest : PlayFabRequestCommon
	{
		// Token: 0x04000FB7 RID: 4023
		public string CatalogVersion;

		// Token: 0x04000FB8 RID: 4024
		public string CurrencyCode;

		// Token: 0x04000FB9 RID: 4025
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000FBA RID: 4026
		public uint? PurchasePrice;

		// Token: 0x04000FBB RID: 4027
		public string ReceiptJson;

		// Token: 0x04000FBC RID: 4028
		public string Signature;
	}
}
