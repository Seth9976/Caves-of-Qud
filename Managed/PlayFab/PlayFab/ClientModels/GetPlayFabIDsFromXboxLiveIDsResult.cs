using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000239 RID: 569
	[Serializable]
	public class GetPlayFabIDsFromXboxLiveIDsResult : PlayFabResultCommon
	{
		// Token: 0x04000C73 RID: 3187
		public List<XboxLiveAccountPlayFabIdPair> Data;
	}
}
