using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public class ListMultiplayerServersResponse : PlayFabResultCommon
	{
		// Token: 0x0400050F RID: 1295
		public List<MultiplayerServerSummary> MultiplayerServerSummaries;

		// Token: 0x04000510 RID: 1296
		public int PageSize;

		// Token: 0x04000511 RID: 1297
		public string SkipToken;
	}
}
