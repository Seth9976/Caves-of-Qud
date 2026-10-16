using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200026B RID: 619
	[Serializable]
	public class LinkNintendoServiceAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CE9 RID: 3305
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CEA RID: 3306
		public bool? ForceLink;

		// Token: 0x04000CEB RID: 3307
		public string IdentityToken;
	}
}
