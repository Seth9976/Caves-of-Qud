using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	public class DeleteBuildRequest : PlayFabRequestCommon
	{
		// Token: 0x0400044B RID: 1099
		public string BuildId;

		// Token: 0x0400044C RID: 1100
		public Dictionary<string, string> CustomTags;
	}
}
