using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	public class Scorecard : PlayFabBaseModel
	{
		// Token: 0x040006C9 RID: 1737
		public string DateGenerated;

		// Token: 0x040006CA RID: 1738
		public string Duration;

		// Token: 0x040006CB RID: 1739
		public double EventsProcessed;

		// Token: 0x040006CC RID: 1740
		public string ExperimentId;

		// Token: 0x040006CD RID: 1741
		public string ExperimentName;

		// Token: 0x040006CE RID: 1742
		public AnalysisTaskState? LatestJobStatus;

		// Token: 0x040006CF RID: 1743
		public bool SampleRatioMismatch;

		// Token: 0x040006D0 RID: 1744
		public List<ScorecardDataRow> ScorecardDataRows;
	}
}
