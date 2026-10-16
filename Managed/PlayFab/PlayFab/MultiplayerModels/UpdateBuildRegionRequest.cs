using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public class UpdateBuildRegionRequest : PlayFabRequestCommon
	{
		// Token: 0x040005C2 RID: 1474
		public string BuildId;

		// Token: 0x040005C3 RID: 1475
		public BuildRegionParams BuildRegion;

		// Token: 0x040005C4 RID: 1476
		public Dictionary<string, string> CustomTags;
	}
}
