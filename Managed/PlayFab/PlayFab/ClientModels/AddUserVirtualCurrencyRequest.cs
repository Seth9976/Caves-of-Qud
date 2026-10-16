using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	public class AddUserVirtualCurrencyRequest : PlayFabRequestCommon
	{
		// Token: 0x0400096F RID: 2415
		public int Amount;

		// Token: 0x04000970 RID: 2416
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000971 RID: 2417
		public string VirtualCurrency;
	}
}
