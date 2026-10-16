using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001C9 RID: 457
	[Serializable]
	public class CatalogItemConsumableInfo : PlayFabBaseModel
	{
		// Token: 0x040009A5 RID: 2469
		public uint? UsageCount;

		// Token: 0x040009A6 RID: 2470
		public uint? UsagePeriod;

		// Token: 0x040009A7 RID: 2471
		public string UsagePeriodGroup;
	}
}
