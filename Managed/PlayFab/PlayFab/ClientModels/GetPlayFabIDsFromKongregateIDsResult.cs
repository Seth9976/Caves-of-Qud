using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200022F RID: 559
	[Serializable]
	public class GetPlayFabIDsFromKongregateIDsResult : PlayFabResultCommon
	{
		// Token: 0x04000C67 RID: 3175
		public List<KongregatePlayFabIdPair> Data;
	}
}
