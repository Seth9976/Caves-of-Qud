using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002A5 RID: 677
	[Serializable]
	public class PurchaseReceiptFulfillment : PlayFabBaseModel
	{
		// Token: 0x04000E2F RID: 3631
		public List<ItemInstance> FulfilledItems;

		// Token: 0x04000E30 RID: 3632
		public string RecordedPriceSource;

		// Token: 0x04000E31 RID: 3633
		public string RecordedTransactionCurrency;

		// Token: 0x04000E32 RID: 3634
		public uint? RecordedTransactionTotal;
	}
}
