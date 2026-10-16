using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000271 RID: 625
	[Serializable]
	public class LinkSteamAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CF8 RID: 3320
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CF9 RID: 3321
		public bool? ForceLink;

		// Token: 0x04000CFA RID: 3322
		public string SteamTicket;
	}
}
