using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002FA RID: 762
	[Serializable]
	public class UnlinkTwitchAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F24 RID: 3876
		public string AccessToken;

		// Token: 0x04000F25 RID: 3877
		public Dictionary<string, string> CustomTags;
	}
}
