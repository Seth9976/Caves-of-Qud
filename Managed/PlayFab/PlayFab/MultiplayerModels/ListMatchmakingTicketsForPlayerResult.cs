using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public class ListMatchmakingTicketsForPlayerResult : PlayFabResultCommon
	{
		// Token: 0x04000509 RID: 1289
		public List<string> TicketIds;
	}
}
