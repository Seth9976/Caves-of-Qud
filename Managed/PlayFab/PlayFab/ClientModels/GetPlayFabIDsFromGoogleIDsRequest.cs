using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200022C RID: 556
	[Serializable]
	public class GetPlayFabIDsFromGoogleIDsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C64 RID: 3172
		public List<string> GoogleIDs;
	}
}
