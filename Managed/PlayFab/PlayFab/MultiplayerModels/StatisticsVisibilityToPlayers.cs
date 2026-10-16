using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000F4 RID: 244
	[Serializable]
	public class StatisticsVisibilityToPlayers : PlayFabBaseModel
	{
		// Token: 0x0400059D RID: 1437
		public bool ShowNumberOfPlayersMatching;

		// Token: 0x0400059E RID: 1438
		public bool ShowTimeToMatch;
	}
}
