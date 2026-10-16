using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	public class MatchmakingPlayer : PlayFabBaseModel
	{
		// Token: 0x0400052B RID: 1323
		public MatchmakingPlayerAttributes Attributes;

		// Token: 0x0400052C RID: 1324
		public EntityKey Entity;
	}
}
