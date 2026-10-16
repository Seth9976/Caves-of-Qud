using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001D1 RID: 465
	[Serializable]
	public class ConfirmPurchaseResult : PlayFabResultCommon
	{
		// Token: 0x040009C0 RID: 2496
		public List<ItemInstance> Items;

		// Token: 0x040009C1 RID: 2497
		public string OrderId;

		// Token: 0x040009C2 RID: 2498
		public DateTime PurchaseDate;
	}
}
