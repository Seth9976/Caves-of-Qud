using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public class StringEqualityRule : PlayFabBaseModel
	{
		// Token: 0x0400059F RID: 1439
		public QueueRuleAttribute Attribute;

		// Token: 0x040005A0 RID: 1440
		public AttributeNotSpecifiedBehavior AttributeNotSpecifiedBehavior;

		// Token: 0x040005A1 RID: 1441
		public string DefaultAttributeValue;

		// Token: 0x040005A2 RID: 1442
		public StringEqualityRuleExpansion Expansion;

		// Token: 0x040005A3 RID: 1443
		public string Name;

		// Token: 0x040005A4 RID: 1444
		public uint? SecondsUntilOptional;

		// Token: 0x040005A5 RID: 1445
		public double Weight;
	}
}
