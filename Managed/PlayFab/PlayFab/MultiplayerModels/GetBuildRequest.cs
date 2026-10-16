using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200009A RID: 154
	[Serializable]
	public class GetBuildRequest : PlayFabRequestCommon
	{
		// Token: 0x04000473 RID: 1139
		public string BuildId;

		// Token: 0x04000474 RID: 1140
		public Dictionary<string, string> CustomTags;
	}
}
