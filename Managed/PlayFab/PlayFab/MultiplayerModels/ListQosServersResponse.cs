using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000D1 RID: 209
	[Serializable]
	public class ListQosServersResponse : PlayFabResultCommon
	{
		// Token: 0x0400051C RID: 1308
		public int PageSize;

		// Token: 0x0400051D RID: 1309
		public List<QosServer> QosServers;

		// Token: 0x0400051E RID: 1310
		public string SkipToken;
	}
}
