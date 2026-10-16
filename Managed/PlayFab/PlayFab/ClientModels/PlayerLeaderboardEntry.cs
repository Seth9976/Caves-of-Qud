using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200029E RID: 670
	[Serializable]
	public class PlayerLeaderboardEntry : PlayFabBaseModel
	{
		// Token: 0x04000DF5 RID: 3573
		public string DisplayName;

		// Token: 0x04000DF6 RID: 3574
		public string PlayFabId;

		// Token: 0x04000DF7 RID: 3575
		public int Position;

		// Token: 0x04000DF8 RID: 3576
		public PlayerProfileModel Profile;

		// Token: 0x04000DF9 RID: 3577
		public int StatValue;
	}
}
