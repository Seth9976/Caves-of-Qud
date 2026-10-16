using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000221 RID: 545
	[Serializable]
	public class GetPlayerTagsResult : PlayFabResultCommon
	{
		// Token: 0x04000C57 RID: 3159
		public string PlayFabId;

		// Token: 0x04000C58 RID: 3160
		public List<string> Tags;
	}
}
