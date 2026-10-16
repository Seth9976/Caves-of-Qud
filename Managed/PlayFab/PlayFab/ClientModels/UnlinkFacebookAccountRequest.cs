using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002E6 RID: 742
	[Serializable]
	public class UnlinkFacebookAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F15 RID: 3861
		public Dictionary<string, string> CustomTags;
	}
}
