using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000E6 RID: 230
	[Serializable]
	public class RegionSelectionRule : PlayFabBaseModel
	{
		// Token: 0x04000566 RID: 1382
		public CustomRegionSelectionRuleExpansion CustomExpansion;

		// Token: 0x04000567 RID: 1383
		public LinearRegionSelectionRuleExpansion LinearExpansion;

		// Token: 0x04000568 RID: 1384
		public uint MaxLatency;

		// Token: 0x04000569 RID: 1385
		public string Name;

		// Token: 0x0400056A RID: 1386
		public string Path;

		// Token: 0x0400056B RID: 1387
		public uint? SecondsUntilOptional;

		// Token: 0x0400056C RID: 1388
		public double Weight;
	}
}
