using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002F5 RID: 757
	[Serializable]
	public class UnlinkOpenIdConnectRequest : PlayFabRequestCommon
	{
		// Token: 0x04000F20 RID: 3872
		public string ConnectionId;

		// Token: 0x04000F21 RID: 3873
		public Dictionary<string, string> CustomTags;
	}
}
