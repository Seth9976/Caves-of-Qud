using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200032C RID: 812
	[Serializable]
	public class ValidateIOSReceiptRequest : PlayFabRequestCommon
	{
		// Token: 0x04000FBE RID: 4030
		public string CatalogVersion;

		// Token: 0x04000FBF RID: 4031
		public string CurrencyCode;

		// Token: 0x04000FC0 RID: 4032
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000FC1 RID: 4033
		public int PurchasePrice;

		// Token: 0x04000FC2 RID: 4034
		public string ReceiptData;
	}
}
