using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000211 RID: 529
	[Serializable]
	public class GetPaymentTokenResult : PlayFabResultCommon
	{
		// Token: 0x04000C23 RID: 3107
		public string OrderId;

		// Token: 0x04000C24 RID: 3108
		public string ProviderToken;
	}
}
