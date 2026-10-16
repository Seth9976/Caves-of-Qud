using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	public class VirtualMachineSummary : PlayFabBaseModel
	{
		// Token: 0x040005CA RID: 1482
		public string HealthStatus;

		// Token: 0x040005CB RID: 1483
		public string State;

		// Token: 0x040005CC RID: 1484
		public string VmId;
	}
}
