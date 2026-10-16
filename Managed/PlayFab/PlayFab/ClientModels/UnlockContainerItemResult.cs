using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000302 RID: 770
	[Serializable]
	public class UnlockContainerItemResult : PlayFabResultCommon
	{
		// Token: 0x04000F32 RID: 3890
		public List<ItemInstance> GrantedItems;

		// Token: 0x04000F33 RID: 3891
		public string UnlockedItemInstanceId;

		// Token: 0x04000F34 RID: 3892
		public string UnlockedWithItemInstanceId;

		// Token: 0x04000F35 RID: 3893
		public Dictionary<string, uint> VirtualCurrency;
	}
}
