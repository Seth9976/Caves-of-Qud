using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000229 RID: 553
	[Serializable]
	public class GetPlayFabIDsFromGameCenterIDsResult : PlayFabResultCommon
	{
		// Token: 0x04000C61 RID: 3169
		public List<GameCenterPlayFabIdPair> Data;
	}
}
