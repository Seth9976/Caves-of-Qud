using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000232 RID: 562
	[Serializable]
	public class GetPlayFabIDsFromPSNAccountIDsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C6A RID: 3178
		public int? IssuerId;

		// Token: 0x04000C6B RID: 3179
		public List<string> PSNAccountIDs;
	}
}
