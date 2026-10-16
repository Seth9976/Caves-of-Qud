using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	public class CurrentGamesResult : PlayFabResultCommon
	{
		// Token: 0x04000B81 RID: 2945
		public int GameCount;

		// Token: 0x04000B82 RID: 2946
		public List<GameInfo> Games;

		// Token: 0x04000B83 RID: 2947
		public int PlayerCount;
	}
}
