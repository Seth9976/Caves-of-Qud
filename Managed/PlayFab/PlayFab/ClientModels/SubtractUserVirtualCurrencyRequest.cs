using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002D8 RID: 728
	[Serializable]
	public class SubtractUserVirtualCurrencyRequest : PlayFabRequestCommon
	{
		// Token: 0x04000ED4 RID: 3796
		public int Amount;

		// Token: 0x04000ED5 RID: 3797
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000ED6 RID: 3798
		public string VirtualCurrency;
	}
}
