using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200032E RID: 814
	[Serializable]
	public class ValidateWindowsReceiptRequest : PlayFabRequestCommon
	{
		// Token: 0x04000FC4 RID: 4036
		public string CatalogVersion;

		// Token: 0x04000FC5 RID: 4037
		public string CurrencyCode;

		// Token: 0x04000FC6 RID: 4038
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000FC7 RID: 4039
		public uint PurchasePrice;

		// Token: 0x04000FC8 RID: 4040
		public string Receipt;
	}
}
