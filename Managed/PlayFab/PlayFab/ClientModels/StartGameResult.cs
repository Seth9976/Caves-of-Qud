using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002CC RID: 716
	[Serializable]
	public class StartGameResult : PlayFabResultCommon
	{
		// Token: 0x04000E9F RID: 3743
		public string Expires;

		// Token: 0x04000EA0 RID: 3744
		public string LobbyID;

		// Token: 0x04000EA1 RID: 3745
		public string Password;

		// Token: 0x04000EA2 RID: 3746
		public string ServerIPV4Address;

		// Token: 0x04000EA3 RID: 3747
		public string ServerIPV6Address;

		// Token: 0x04000EA4 RID: 3748
		public int? ServerPort;

		// Token: 0x04000EA5 RID: 3749
		public string ServerPublicDNSName;

		// Token: 0x04000EA6 RID: 3750
		public string Ticket;
	}
}
