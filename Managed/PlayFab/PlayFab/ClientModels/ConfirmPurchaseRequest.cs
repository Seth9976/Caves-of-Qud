using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001D0 RID: 464
	[Serializable]
	public class ConfirmPurchaseRequest : PlayFabRequestCommon
	{
		// Token: 0x040009BE RID: 2494
		public Dictionary<string, string> CustomTags;

		// Token: 0x040009BF RID: 2495
		public string OrderId;
	}
}
