using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000141 RID: 321
	[Serializable]
	public class CreateExperimentRequest : PlayFabRequestCommon
	{
		// Token: 0x04000694 RID: 1684
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000695 RID: 1685
		public string Description;

		// Token: 0x04000696 RID: 1686
		public uint Duration;

		// Token: 0x04000697 RID: 1687
		public ExperimentType? ExperimentType;

		// Token: 0x04000698 RID: 1688
		public string Name;

		// Token: 0x04000699 RID: 1689
		public string SegmentId;

		// Token: 0x0400069A RID: 1690
		public DateTime StartDate;

		// Token: 0x0400069B RID: 1691
		public List<string> TitlePlayerAccountTestIds;

		// Token: 0x0400069C RID: 1692
		public List<Variant> Variants;
	}
}
