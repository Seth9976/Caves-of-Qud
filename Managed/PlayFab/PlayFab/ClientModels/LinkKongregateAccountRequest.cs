using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000269 RID: 617
	[Serializable]
	public class LinkKongregateAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CE5 RID: 3301
		public string AuthTicket;

		// Token: 0x04000CE6 RID: 3302
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CE7 RID: 3303
		public bool? ForceLink;

		// Token: 0x04000CE8 RID: 3304
		public string KongregateId;
	}
}
