using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001C8 RID: 456
	[Serializable]
	public class CatalogItemBundleInfo : PlayFabBaseModel
	{
		// Token: 0x040009A2 RID: 2466
		public List<string> BundledItems;

		// Token: 0x040009A3 RID: 2467
		public List<string> BundledResultTables;

		// Token: 0x040009A4 RID: 2468
		public Dictionary<string, uint> BundledVirtualCurrencies;
	}
}
