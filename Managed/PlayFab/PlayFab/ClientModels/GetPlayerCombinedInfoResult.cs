using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000216 RID: 534
	[Serializable]
	public class GetPlayerCombinedInfoResult : PlayFabResultCommon
	{
		// Token: 0x04000C39 RID: 3129
		public GetPlayerCombinedInfoResultPayload InfoResultPayload;

		// Token: 0x04000C3A RID: 3130
		public string PlayFabId;
	}
}
