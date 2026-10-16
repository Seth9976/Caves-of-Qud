using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x02000151 RID: 337
	[Serializable]
	public class ScorecardDataRow : PlayFabBaseModel
	{
		// Token: 0x040006D1 RID: 1745
		public bool IsControl;

		// Token: 0x040006D2 RID: 1746
		public Dictionary<string, MetricData> MetricDataRows;

		// Token: 0x040006D3 RID: 1747
		public uint PlayerCount;

		// Token: 0x040006D4 RID: 1748
		public string VariantName;
	}
}
