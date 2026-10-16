using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000296 RID: 662
	[Serializable]
	public class ModifyUserVirtualCurrencyResult : PlayFabResultCommon
	{
		// Token: 0x04000DD6 RID: 3542
		public int Balance;

		// Token: 0x04000DD7 RID: 3543
		public int BalanceChange;

		// Token: 0x04000DD8 RID: 3544
		public string PlayFabId;

		// Token: 0x04000DD9 RID: 3545
		public string VirtualCurrency;
	}
}
