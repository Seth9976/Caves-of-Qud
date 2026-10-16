using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000233 RID: 563
	[Serializable]
	public class GetPlayFabIDsFromPSNAccountIDsResult : PlayFabResultCommon
	{
		// Token: 0x04000C6C RID: 3180
		public List<PSNAccountPlayFabIdPair> Data;
	}
}
