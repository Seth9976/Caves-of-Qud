using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	public class AdRewardResults : PlayFabBaseModel
	{
		// Token: 0x0400097E RID: 2430
		public List<AdRewardItemGranted> GrantedItems;

		// Token: 0x0400097F RID: 2431
		public Dictionary<string, int> GrantedVirtualCurrencies;

		// Token: 0x04000980 RID: 2432
		public Dictionary<string, int> IncrementedStatistics;
	}
}
