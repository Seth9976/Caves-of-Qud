using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002FC RID: 764
	[Serializable]
	public class UnlinkWindowsHelloAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F26 RID: 3878
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000F27 RID: 3879
		public string PublicKeyHint;
	}
}
