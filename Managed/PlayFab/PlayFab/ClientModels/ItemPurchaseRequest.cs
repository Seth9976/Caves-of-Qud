using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000257 RID: 599
	[Serializable]
	public class ItemPurchaseRequest : PlayFabBaseModel
	{
		// Token: 0x04000CBB RID: 3259
		public string Annotation;

		// Token: 0x04000CBC RID: 3260
		public string ItemId;

		// Token: 0x04000CBD RID: 3261
		public uint Quantity;

		// Token: 0x04000CBE RID: 3262
		public List<string> UpgradeFromItems;
	}
}
