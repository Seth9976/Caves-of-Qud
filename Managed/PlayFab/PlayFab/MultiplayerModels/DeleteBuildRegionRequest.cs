using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	public class DeleteBuildRegionRequest : PlayFabRequestCommon
	{
		// Token: 0x04000448 RID: 1096
		public string BuildId;

		// Token: 0x04000449 RID: 1097
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400044A RID: 1098
		public string Region;
	}
}
