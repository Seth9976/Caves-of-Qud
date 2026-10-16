using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000228 RID: 552
	[Serializable]
	public class GetPlayFabIDsFromGameCenterIDsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C60 RID: 3168
		public List<string> GameCenterIDs;
	}
}
