using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002A9 RID: 681
	[Serializable]
	public class RedeemCouponResult : PlayFabResultCommon
	{
		// Token: 0x04000E3C RID: 3644
		public List<ItemInstance> GrantedItems;
	}
}
