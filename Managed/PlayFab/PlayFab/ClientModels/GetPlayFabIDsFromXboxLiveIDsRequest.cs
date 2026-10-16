using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000238 RID: 568
	[Serializable]
	public class GetPlayFabIDsFromXboxLiveIDsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C71 RID: 3185
		public string Sandbox;

		// Token: 0x04000C72 RID: 3186
		public List<string> XboxLiveAccountIDs;
	}
}
