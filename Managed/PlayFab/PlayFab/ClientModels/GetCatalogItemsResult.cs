using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001F8 RID: 504
	[Serializable]
	public class GetCatalogItemsResult : PlayFabResultCommon
	{
		// Token: 0x04000BD3 RID: 3027
		public List<CatalogItem> Catalog;
	}
}
