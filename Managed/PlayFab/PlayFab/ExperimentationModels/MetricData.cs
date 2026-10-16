using System;
using PlayFab.SharedModels;

namespace PlayFab.ExperimentationModels
{
	// Token: 0x0200014F RID: 335
	[Serializable]
	public class MetricData : PlayFabBaseModel
	{
		// Token: 0x040006BC RID: 1724
		public double ConfidenceIntervalEnd;

		// Token: 0x040006BD RID: 1725
		public double ConfidenceIntervalStart;

		// Token: 0x040006BE RID: 1726
		public float DeltaAbsoluteChange;

		// Token: 0x040006BF RID: 1727
		public float DeltaRelativeChange;

		// Token: 0x040006C0 RID: 1728
		public string InternalName;

		// Token: 0x040006C1 RID: 1729
		public string Movement;

		// Token: 0x040006C2 RID: 1730
		public string Name;

		// Token: 0x040006C3 RID: 1731
		public float PMove;

		// Token: 0x040006C4 RID: 1732
		public float PValue;

		// Token: 0x040006C5 RID: 1733
		public float PValueThreshold;

		// Token: 0x040006C6 RID: 1734
		public string StatSigLevel;

		// Token: 0x040006C7 RID: 1735
		public float StdDev;

		// Token: 0x040006C8 RID: 1736
		public float Value;
	}
}
