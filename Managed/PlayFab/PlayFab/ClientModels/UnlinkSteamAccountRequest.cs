using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002F8 RID: 760
	[Serializable]
	public class UnlinkSteamAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F23 RID: 3875
		public Dictionary<string, string> CustomTags;
	}
}
