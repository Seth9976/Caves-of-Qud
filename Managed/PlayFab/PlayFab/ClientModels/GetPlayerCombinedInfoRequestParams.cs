using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000215 RID: 533
	[Serializable]
	public class GetPlayerCombinedInfoRequestParams : PlayFabBaseModel
	{
		// Token: 0x04000C2A RID: 3114
		public bool GetCharacterInventories;

		// Token: 0x04000C2B RID: 3115
		public bool GetCharacterList;

		// Token: 0x04000C2C RID: 3116
		public bool GetPlayerProfile;

		// Token: 0x04000C2D RID: 3117
		public bool GetPlayerStatistics;

		// Token: 0x04000C2E RID: 3118
		public bool GetTitleData;

		// Token: 0x04000C2F RID: 3119
		public bool GetUserAccountInfo;

		// Token: 0x04000C30 RID: 3120
		public bool GetUserData;

		// Token: 0x04000C31 RID: 3121
		public bool GetUserInventory;

		// Token: 0x04000C32 RID: 3122
		public bool GetUserReadOnlyData;

		// Token: 0x04000C33 RID: 3123
		public bool GetUserVirtualCurrency;

		// Token: 0x04000C34 RID: 3124
		public List<string> PlayerStatisticNames;

		// Token: 0x04000C35 RID: 3125
		public PlayerProfileViewConstraints ProfileConstraints;

		// Token: 0x04000C36 RID: 3126
		public List<string> TitleDataKeys;

		// Token: 0x04000C37 RID: 3127
		public List<string> UserDataKeys;

		// Token: 0x04000C38 RID: 3128
		public List<string> UserReadOnlyDataKeys;
	}
}
