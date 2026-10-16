using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000CD RID: 205
	[Serializable]
	public class ListPartyQosServersResponse : PlayFabResultCommon
	{
		// Token: 0x04000514 RID: 1300
		public int PageSize;

		// Token: 0x04000515 RID: 1301
		public List<QosServer> QosServers;

		// Token: 0x04000516 RID: 1302
		public string SkipToken;
	}
}
