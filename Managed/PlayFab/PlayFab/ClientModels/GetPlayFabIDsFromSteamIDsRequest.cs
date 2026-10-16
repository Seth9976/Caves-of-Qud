using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000234 RID: 564
	[Serializable]
	public class GetPlayFabIDsFromSteamIDsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C6D RID: 3181
		public List<string> SteamStringIDs;
	}
}
