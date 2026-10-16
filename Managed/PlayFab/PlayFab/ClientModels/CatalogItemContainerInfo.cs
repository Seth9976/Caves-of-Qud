using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001CA RID: 458
	[Serializable]
	public class CatalogItemContainerInfo : PlayFabBaseModel
	{
		// Token: 0x040009A8 RID: 2472
		public List<string> ItemContents;

		// Token: 0x040009A9 RID: 2473
		public string KeyItemId;

		// Token: 0x040009AA RID: 2474
		public List<string> ResultTableContents;

		// Token: 0x040009AB RID: 2475
		public Dictionary<string, uint> VirtualCurrencyContents;
	}
}
