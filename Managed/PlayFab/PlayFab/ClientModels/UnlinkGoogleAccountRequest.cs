using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002EC RID: 748
	[Serializable]
	public class UnlinkGoogleAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F19 RID: 3865
		public Dictionary<string, string> CustomTags;
	}
}
