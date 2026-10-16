using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public class LinearRegionSelectionRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x040004DD RID: 1245
		public uint Delta;

		// Token: 0x040004DE RID: 1246
		public uint Limit;

		// Token: 0x040004DF RID: 1247
		public uint SecondsBetweenExpansions;
	}
}
