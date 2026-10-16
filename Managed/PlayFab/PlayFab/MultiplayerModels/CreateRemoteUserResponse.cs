using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200007D RID: 125
	[Serializable]
	public class CreateRemoteUserResponse : PlayFabResultCommon
	{
		// Token: 0x04000429 RID: 1065
		public DateTime? ExpirationTime;

		// Token: 0x0400042A RID: 1066
		public string Password;

		// Token: 0x0400042B RID: 1067
		public string Username;
	}
}
