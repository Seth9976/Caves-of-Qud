using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000146 RID: 326
	[Serializable]
	public class Experiment : PlayFabBaseModel
	{
		// Token: 0x040006A2 RID: 1698
		public string Description;

		// Token: 0x040006A3 RID: 1699
		public uint Duration;

		// Token: 0x040006A4 RID: 1700
		public ExperimentType? ExperimentType;

		// Token: 0x040006A5 RID: 1701
		public string Id;

		// Token: 0x040006A6 RID: 1702
		public string Name;

		// Token: 0x040006A7 RID: 1703
		public string SegmentId;

		// Token: 0x040006A8 RID: 1704
		public DateTime StartDate;

		// Token: 0x040006A9 RID: 1705
		public ExperimentState? State;

		// Token: 0x040006AA RID: 1706
		public List<string> TitlePlayerAccountTestIds;

		// Token: 0x040006AB RID: 1707
		public List<Variant> Variants;
	}
}
