using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200025C RID: 604
	[Serializable]
	public class LinkCustomIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CC9 RID: 3273
		public string CustomId;

		// Token: 0x04000CCA RID: 3274
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CCB RID: 3275
		public bool? ForceLink;
	}
}
