using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200030E RID: 782
	[Serializable]
	public class UpdateUserTitleDisplayNameRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F4C RID: 3916
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F4D RID: 3917
		public string DisplayName;
	}
}
