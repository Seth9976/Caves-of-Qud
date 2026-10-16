using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000218 RID: 536
	[Serializable]
	public class GetPlayerProfileRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C48 RID: 3144
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000C49 RID: 3145
		public string PlayFabId;

		// Token: 0x04000C4A RID: 3146
		public PlayerProfileViewConstraints ProfileConstraints;
	}
}
