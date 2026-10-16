using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class DifferenceRule : PlayFabBaseModel
	{
		// Token: 0x04000456 RID: 1110
		public QueueRuleAttribute Attribute;

		// Token: 0x04000457 RID: 1111
		public AttributeNotSpecifiedBehavior AttributeNotSpecifiedBehavior;

		// Token: 0x04000458 RID: 1112
		public CustomDifferenceRuleExpansion CustomExpansion;

		// Token: 0x04000459 RID: 1113
		public double? DefaultAttributeValue;

		// Token: 0x0400045A RID: 1114
		public double Difference;

		// Token: 0x0400045B RID: 1115
		public LinearDifferenceRuleExpansion LinearExpansion;

		// Token: 0x0400045C RID: 1116
		public AttributeMergeFunction MergeFunction;

		// Token: 0x0400045D RID: 1117
		public string Name;

		// Token: 0x0400045E RID: 1118
		public uint? SecondsUntilOptional;

		// Token: 0x0400045F RID: 1119
		public double Weight;
	}
}
