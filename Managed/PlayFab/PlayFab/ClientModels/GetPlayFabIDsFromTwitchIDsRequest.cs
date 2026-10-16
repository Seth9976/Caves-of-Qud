using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000236 RID: 566
	[Serializable]
	public class GetPlayFabIDsFromTwitchIDsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C6F RID: 3183
		public List<string> TwitchIds;
	}
}
