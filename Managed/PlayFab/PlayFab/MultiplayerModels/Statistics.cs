using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public class Statistics : PlayFabBaseModel
	{
		// Token: 0x04000599 RID: 1433
		public double Average;

		// Token: 0x0400059A RID: 1434
		public double Percentile50;

		// Token: 0x0400059B RID: 1435
		public double Percentile90;

		// Token: 0x0400059C RID: 1436
		public double Percentile99;
	}
}
