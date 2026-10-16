using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000277 RID: 631
	[Serializable]
	public class LinkXboxAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000D03 RID: 3331
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000D04 RID: 3332
		public bool? ForceLink;

		// Token: 0x04000D05 RID: 3333
		public string XboxToken;
	}
}
