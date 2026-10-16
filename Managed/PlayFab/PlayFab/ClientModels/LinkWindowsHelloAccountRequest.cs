using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000275 RID: 629
	[Serializable]
	public class LinkWindowsHelloAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CFE RID: 3326
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CFF RID: 3327
		public string DeviceName;

		// Token: 0x04000D00 RID: 3328
		public bool? ForceLink;

		// Token: 0x04000D01 RID: 3329
		public string PublicKey;

		// Token: 0x04000D02 RID: 3330
		public string UserName;
	}
}
