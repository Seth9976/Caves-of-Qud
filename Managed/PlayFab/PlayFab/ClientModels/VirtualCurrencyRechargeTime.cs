using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000332 RID: 818
	[Serializable]
	public class VirtualCurrencyRechargeTime : PlayFabBaseModel
	{
		// Token: 0x04000FCF RID: 4047
		public int RechargeMax;

		// Token: 0x04000FD0 RID: 4048
		public DateTime RechargeTime;

		// Token: 0x04000FD1 RID: 4049
		public int SecondsToRecharge;
	}
}
