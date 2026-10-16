using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000DA RID: 218
	[Serializable]
	public class MatchmakingQueueTeam : PlayFabBaseModel
	{
		// Token: 0x04000542 RID: 1346
		public uint MaxTeamSize;

		// Token: 0x04000543 RID: 1347
		public uint MinTeamSize;

		// Token: 0x04000544 RID: 1348
		public string Name;
	}
}
