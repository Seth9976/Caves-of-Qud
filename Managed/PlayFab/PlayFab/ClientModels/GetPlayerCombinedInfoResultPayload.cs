using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000217 RID: 535
	[Serializable]
	public class GetPlayerCombinedInfoResultPayload : PlayFabBaseModel
	{
		// Token: 0x04000C3B RID: 3131
		public UserAccountInfo AccountInfo;

		// Token: 0x04000C3C RID: 3132
		public List<CharacterInventory> CharacterInventories;

		// Token: 0x04000C3D RID: 3133
		public List<CharacterResult> CharacterList;

		// Token: 0x04000C3E RID: 3134
		public PlayerProfileModel PlayerProfile;

		// Token: 0x04000C3F RID: 3135
		public List<StatisticValue> PlayerStatistics;

		// Token: 0x04000C40 RID: 3136
		public Dictionary<string, string> TitleData;

		// Token: 0x04000C41 RID: 3137
		public Dictionary<string, UserDataRecord> UserData;

		// Token: 0x04000C42 RID: 3138
		public uint UserDataVersion;

		// Token: 0x04000C43 RID: 3139
		public List<ItemInstance> UserInventory;

		// Token: 0x04000C44 RID: 3140
		public Dictionary<string, UserDataRecord> UserReadOnlyData;

		// Token: 0x04000C45 RID: 3141
		public uint UserReadOnlyDataVersion;

		// Token: 0x04000C46 RID: 3142
		public Dictionary<string, int> UserVirtualCurrency;

		// Token: 0x04000C47 RID: 3143
		public Dictionary<string, VirtualCurrencyRechargeTime> UserVirtualCurrencyRechargeTimes;
	}
}
