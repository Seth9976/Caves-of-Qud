using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002E3 RID: 739
	[Serializable]
	public class UnlinkAppleRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F12 RID: 3858
		public Dictionary<string, string> CustomTags;
	}
}
