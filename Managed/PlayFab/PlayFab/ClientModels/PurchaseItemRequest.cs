using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002A3 RID: 675
	[Serializable]
	public class PurchaseItemRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E27 RID: 3623
		public string CatalogVersion;

		// Token: 0x04000E28 RID: 3624
		public string CharacterId;

		// Token: 0x04000E29 RID: 3625
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000E2A RID: 3626
		public string ItemId;

		// Token: 0x04000E2B RID: 3627
		public int Price;

		// Token: 0x04000E2C RID: 3628
		public string StoreId;

		// Token: 0x04000E2D RID: 3629
		public string VirtualCurrency;
	}
}
