using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000AF RID: 175
	[Serializable]
	public class GetTitleEnabledForMultiplayerServersStatusRequest : PlayFabRequestCommon
	{
		// Token: 0x040004D1 RID: 1233
		public Dictionary<string, string> CustomTags;
	}
}
