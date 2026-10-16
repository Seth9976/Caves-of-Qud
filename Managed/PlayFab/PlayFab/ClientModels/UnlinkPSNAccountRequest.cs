using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002F6 RID: 758
	[Serializable]
	public class UnlinkPSNAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F22 RID: 3874
		public Dictionary<string, string> CustomTags;
	}
}
