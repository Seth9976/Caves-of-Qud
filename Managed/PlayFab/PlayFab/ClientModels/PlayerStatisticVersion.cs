using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002A1 RID: 673
	[Serializable]
	public class PlayerStatisticVersion : PlayFabBaseModel
	{
		// Token: 0x04000E1F RID: 3615
		public DateTime ActivationTime;

		// Token: 0x04000E20 RID: 3616
		public DateTime? DeactivationTime;

		// Token: 0x04000E21 RID: 3617
		public DateTime? ScheduledActivationTime;

		// Token: 0x04000E22 RID: 3618
		public DateTime? ScheduledDeactivationTime;

		// Token: 0x04000E23 RID: 3619
		public string StatisticName;

		// Token: 0x04000E24 RID: 3620
		public uint Version;
	}
}
