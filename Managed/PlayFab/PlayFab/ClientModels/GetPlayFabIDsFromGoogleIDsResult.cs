using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200022D RID: 557
	[Serializable]
	public class GetPlayFabIDsFromGoogleIDsResult : PlayFabResultCommon
	{
		// Token: 0x04000C65 RID: 3173
		public List<GooglePlayFabIdPair> Data;
	}
}
