using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000293 RID: 659
	[Serializable]
	public class MatchmakeResult : PlayFabResultCommon
	{
		// Token: 0x04000DC2 RID: 3522
		public string Expires;

		// Token: 0x04000DC3 RID: 3523
		public string LobbyID;

		// Token: 0x04000DC4 RID: 3524
		public int? PollWaitTimeMS;

		// Token: 0x04000DC5 RID: 3525
		public string ServerIPV4Address;

		// Token: 0x04000DC6 RID: 3526
		public string ServerIPV6Address;

		// Token: 0x04000DC7 RID: 3527
		public int? ServerPort;

		// Token: 0x04000DC8 RID: 3528
		public string ServerPublicDNSName;

		// Token: 0x04000DC9 RID: 3529
		public MatchmakeStatus? Status;

		// Token: 0x04000DCA RID: 3530
		public string Ticket;
	}
}
