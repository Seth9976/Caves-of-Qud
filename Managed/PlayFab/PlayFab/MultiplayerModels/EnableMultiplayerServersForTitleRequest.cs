using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000092 RID: 146
	[Serializable]
	public class EnableMultiplayerServersForTitleRequest : PlayFabRequestCommon
	{
		// Token: 0x04000465 RID: 1125
		public Dictionary<string, string> CustomTags;
	}
}
