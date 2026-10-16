using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000DB RID: 219
	[Serializable]
	public class MatchTotalRule : PlayFabBaseModel
	{
		// Token: 0x04000545 RID: 1349
		public QueueRuleAttribute Attribute;

		// Token: 0x04000546 RID: 1350
		public MatchTotalRuleExpansion Expansion;

		// Token: 0x04000547 RID: 1351
		public double Max;

		// Token: 0x04000548 RID: 1352
		public double Min;

		// Token: 0x04000549 RID: 1353
		public string Name;

		// Token: 0x0400054A RID: 1354
		public uint? SecondsUntilOptional;

		// Token: 0x0400054B RID: 1355
		public double Weight;
	}
}
