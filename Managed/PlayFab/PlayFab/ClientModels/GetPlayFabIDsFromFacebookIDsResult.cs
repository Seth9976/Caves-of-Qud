using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000225 RID: 549
	[Serializable]
	public class GetPlayFabIDsFromFacebookIDsResult : PlayFabResultCommon
	{
		// Token: 0x04000C5D RID: 3165
		public List<FacebookPlayFabIdPair> Data;
	}
}
