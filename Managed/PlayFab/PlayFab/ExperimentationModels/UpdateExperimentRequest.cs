using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000155 RID: 341
	[Serializable]
	public class UpdateExperimentRequest : PlayFabRequestCommon
	{
		// Token: 0x040006DB RID: 1755
		public Dictionary<string, string> CustomTags;

		// Token: 0x040006DC RID: 1756
		public string Description;

		// Token: 0x040006DD RID: 1757
		public uint Duration;

		// Token: 0x040006DE RID: 1758
		public ExperimentType? ExperimentType;

		// Token: 0x040006DF RID: 1759
		public string Id;

		// Token: 0x040006E0 RID: 1760
		public string Name;

		// Token: 0x040006E1 RID: 1761
		public string SegmentId;

		// Token: 0x040006E2 RID: 1762
		public DateTime StartDate;

		// Token: 0x040006E3 RID: 1763
		public List<string> TitlePlayerAccountTestIds;

		// Token: 0x040006E4 RID: 1764
		public List<Variant> Variants;
	}
}
