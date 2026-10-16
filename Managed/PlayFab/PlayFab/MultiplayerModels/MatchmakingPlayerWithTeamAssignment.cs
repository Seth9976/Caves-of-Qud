using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000D8 RID: 216
	[Serializable]
	public class MatchmakingPlayerWithTeamAssignment : PlayFabBaseModel
	{
		// Token: 0x0400052F RID: 1327
		public MatchmakingPlayerAttributes Attributes;

		// Token: 0x04000530 RID: 1328
		public EntityKey Entity;

		// Token: 0x04000531 RID: 1329
		public string TeamId;
	}
}
