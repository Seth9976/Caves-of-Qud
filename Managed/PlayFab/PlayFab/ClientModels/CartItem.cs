using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	public class CartItem : PlayFabBaseModel
	{
		// Token: 0x04000988 RID: 2440
		public string Description;

		// Token: 0x04000989 RID: 2441
		public string DisplayName;

		// Token: 0x0400098A RID: 2442
		public string ItemClass;

		// Token: 0x0400098B RID: 2443
		public string ItemId;

		// Token: 0x0400098C RID: 2444
		public string ItemInstanceId;

		// Token: 0x0400098D RID: 2445
		public Dictionary<string, uint> RealCurrencyPrices;

		// Token: 0x0400098E RID: 2446
		public Dictionary<string, uint> VCAmount;

		// Token: 0x0400098F RID: 2447
		public Dictionary<string, uint> VirtualCurrencyPrices;
	}
}
