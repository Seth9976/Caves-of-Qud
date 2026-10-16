using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	public class LinearDifferenceRuleExpansion : PlayFabBaseModel
	{
		// Token: 0x040004DA RID: 1242
		public double Delta;

		// Token: 0x040004DB RID: 1243
		public double? Limit;

		// Token: 0x040004DC RID: 1244
		public uint SecondsBetweenExpansions;
	}
}
