using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000227 RID: 551
	[Serializable]
	public class GetPlayFabIDsFromFacebookInstantGamesIdsResult : PlayFabResultCommon
	{
		// Token: 0x04000C5F RID: 3167
		public List<FacebookInstantGamesPlayFabIdPair> Data;
	}
}
