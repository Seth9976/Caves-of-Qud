using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000CF RID: 207
	[Serializable]
	public class ListQosServersForTitleResponse : PlayFabResultCommon
	{
		// Token: 0x04000518 RID: 1304
		public int PageSize;

		// Token: 0x04000519 RID: 1305
		public List<QosServer> QosServers;

		// Token: 0x0400051A RID: 1306
		public string SkipToken;
	}
}
