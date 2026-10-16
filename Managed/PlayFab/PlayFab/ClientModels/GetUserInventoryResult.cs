using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000250 RID: 592
	[Serializable]
	public class GetUserInventoryResult : PlayFabResultCommon
	{
		// Token: 0x04000C9D RID: 3229
		public List<ItemInstance> Inventory;

		// Token: 0x04000C9E RID: 3230
		public Dictionary<string, int> VirtualCurrency;

		// Token: 0x04000C9F RID: 3231
		public Dictionary<string, VirtualCurrencyRechargeTime> VirtualCurrencyRechargeTimes;
	}
}
