using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000226 RID: 550
	[Serializable]
	public class GetPlayFabIDsFromFacebookInstantGamesIdsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C5E RID: 3166
		public List<string> FacebookInstantGamesIds;
	}
}
