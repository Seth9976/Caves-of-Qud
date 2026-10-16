using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002A4 RID: 676
	[Serializable]
	public class PurchaseItemResult : PlayFabResultCommon
	{
		// Token: 0x04000E2E RID: 3630
		public List<ItemInstance> Items;
	}
}
