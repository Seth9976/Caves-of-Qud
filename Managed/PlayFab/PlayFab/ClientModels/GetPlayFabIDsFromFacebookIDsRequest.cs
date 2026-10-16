using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000224 RID: 548
	[Serializable]
	public class GetPlayFabIDsFromFacebookIDsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C5C RID: 3164
		public List<string> FacebookIDs;
	}
}
