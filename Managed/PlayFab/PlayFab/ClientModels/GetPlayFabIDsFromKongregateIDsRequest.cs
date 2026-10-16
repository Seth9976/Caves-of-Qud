using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200022E RID: 558
	[Serializable]
	public class GetPlayFabIDsFromKongregateIDsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C66 RID: 3174
		public List<string> KongregateIDs;
	}
}
