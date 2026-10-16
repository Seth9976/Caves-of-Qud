using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000235 RID: 565
	[Serializable]
	public class GetPlayFabIDsFromSteamIDsResult : PlayFabResultCommon
	{
		// Token: 0x04000C6E RID: 3182
		public List<SteamPlayFabIdPair> Data;
	}
}
