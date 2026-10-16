using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000241 RID: 577
	[Serializable]
	public class GetStoreItemsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C84 RID: 3204
		public string CatalogVersion;

		// Token: 0x04000C85 RID: 3205
		public string StoreId;
	}
}
