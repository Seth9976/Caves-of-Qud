using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002EA RID: 746
	[Serializable]
	public class UnlinkGameCenterAccountRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F18 RID: 3864
		public Dictionary<string, string> CustomTags;
	}
}
