using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	public class CurrentServerStats : PlayFabBaseModel
	{
		// Token: 0x04000436 RID: 1078
		public int Active;

		// Token: 0x04000437 RID: 1079
		public int Propping;

		// Token: 0x04000438 RID: 1080
		public int StandingBy;

		// Token: 0x04000439 RID: 1081
		public int Total;
	}
}
