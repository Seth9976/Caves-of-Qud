using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200027D RID: 637
	[Serializable]
	public class LoginResult : PlayFabLoginResultCommon
	{
		// Token: 0x04000D23 RID: 3363
		public EntityTokenResponse EntityToken;

		// Token: 0x04000D24 RID: 3364
		public GetPlayerCombinedInfoResultPayload InfoResultPayload;

		// Token: 0x04000D25 RID: 3365
		public DateTime? LastLoginTime;

		// Token: 0x04000D26 RID: 3366
		public bool NewlyCreated;

		// Token: 0x04000D27 RID: 3367
		public string PlayFabId;

		// Token: 0x04000D28 RID: 3368
		public string SessionTicket;

		// Token: 0x04000D29 RID: 3369
		public UserSettings SettingsForUser;

		// Token: 0x04000D2A RID: 3370
		public TreatmentAssignment TreatmentAssignment;
	}
}
