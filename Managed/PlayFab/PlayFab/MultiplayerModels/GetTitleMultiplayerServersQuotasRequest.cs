using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public class GetTitleMultiplayerServersQuotasRequest : PlayFabRequestCommon
	{
		// Token: 0x040004D3 RID: 1235
		public Dictionary<string, string> CustomTags;
	}
}
