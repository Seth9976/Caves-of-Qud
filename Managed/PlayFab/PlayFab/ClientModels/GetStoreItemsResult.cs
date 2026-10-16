using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000242 RID: 578
	[Serializable]
	public class GetStoreItemsResult : PlayFabResultCommon
	{
		// Token: 0x04000C86 RID: 3206
		public string CatalogVersion;

		// Token: 0x04000C87 RID: 3207
		public StoreMarketingModel MarketingData;

		// Token: 0x04000C88 RID: 3208
		public SourceType? Source;

		// Token: 0x04000C89 RID: 3209
		public List<StoreItem> Store;

		// Token: 0x04000C8A RID: 3210
		public string StoreId;
	}
}
