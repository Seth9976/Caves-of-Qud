using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002BE RID: 702
	[Serializable]
	public class RestoreIOSPurchasesRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E73 RID: 3699
		public string CatalogVersion;

		// Token: 0x04000E74 RID: 3700
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000E75 RID: 3701
		public string ReceiptData;
	}
}
