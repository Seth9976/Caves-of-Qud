using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001CC RID: 460
	[Serializable]
	public class CharacterLeaderboardEntry : PlayFabBaseModel
	{
		// Token: 0x040009AE RID: 2478
		public string CharacterId;

		// Token: 0x040009AF RID: 2479
		public string CharacterName;

		// Token: 0x040009B0 RID: 2480
		public string CharacterType;

		// Token: 0x040009B1 RID: 2481
		public string DisplayName;

		// Token: 0x040009B2 RID: 2482
		public string PlayFabId;

		// Token: 0x040009B3 RID: 2483
		public int Position;

		// Token: 0x040009B4 RID: 2484
		public int StatValue;
	}
}
