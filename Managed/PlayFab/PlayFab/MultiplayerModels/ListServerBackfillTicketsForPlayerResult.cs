using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000D3 RID: 211
	[Serializable]
	public class ListServerBackfillTicketsForPlayerResult : PlayFabResultCommon
	{
		// Token: 0x04000522 RID: 1314
		public List<string> TicketIds;
	}
}
