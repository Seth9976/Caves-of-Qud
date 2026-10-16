using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000237 RID: 567
	[Serializable]
	public class GetPlayFabIDsFromTwitchIDsResult : PlayFabResultCommon
	{
		// Token: 0x04000C70 RID: 3184
		public List<TwitchPlayFabIdPair> Data;
	}
}
