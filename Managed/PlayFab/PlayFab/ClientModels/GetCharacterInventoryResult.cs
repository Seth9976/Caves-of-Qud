using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001FC RID: 508
	[Serializable]
	public class GetCharacterInventoryResult : PlayFabResultCommon
	{
		// Token: 0x04000BDE RID: 3038
		public string CharacterId;

		// Token: 0x04000BDF RID: 3039
		public List<ItemInstance> Inventory;

		// Token: 0x04000BE0 RID: 3040
		public Dictionary<string, int> VirtualCurrency;

		// Token: 0x04000BE1 RID: 3041
		public Dictionary<string, VirtualCurrencyRechargeTime> VirtualCurrencyRechargeTimes;
	}
}
