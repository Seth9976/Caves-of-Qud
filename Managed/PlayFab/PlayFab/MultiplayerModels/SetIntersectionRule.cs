using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000EF RID: 239
	[Serializable]
	public class SetIntersectionRule : PlayFabBaseModel
	{
		// Token: 0x0400058A RID: 1418
		public QueueRuleAttribute Attribute;

		// Token: 0x0400058B RID: 1419
		public AttributeNotSpecifiedBehavior AttributeNotSpecifiedBehavior;

		// Token: 0x0400058C RID: 1420
		public CustomSetIntersectionRuleExpansion CustomExpansion;

		// Token: 0x0400058D RID: 1421
		public List<string> DefaultAttributeValue;

		// Token: 0x0400058E RID: 1422
		public LinearSetIntersectionRuleExpansion LinearExpansion;

		// Token: 0x0400058F RID: 1423
		public uint MinIntersectionSize;

		// Token: 0x04000590 RID: 1424
		public string Name;

		// Token: 0x04000591 RID: 1425
		public uint? SecondsUntilOptional;

		// Token: 0x04000592 RID: 1426
		public double Weight;
	}
}
