using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002E4 RID: 740
	[Serializable]
	public class UnlinkCustomIDRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F13 RID: 3859
		public string CustomId;

		// Token: 0x04000F14 RID: 3860
		public Dictionary<string, string> CustomTags;
	}
}
