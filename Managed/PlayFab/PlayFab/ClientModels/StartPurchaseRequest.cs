using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002CD RID: 717
	[Serializable]
	public class StartPurchaseRequest : PlayFabRequestCommon
	{
		// Token: 0x04000EA7 RID: 3751
		public string CatalogVersion;

		// Token: 0x04000EA8 RID: 3752
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000EA9 RID: 3753
		public List<ItemPurchaseRequest> Items;

		// Token: 0x04000EAA RID: 3754
		public string StoreId;
	}
}
