using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	public class UpdateBuildRegionsRequest : PlayFabRequestCommon
	{
		// Token: 0x040005C5 RID: 1477
		public string BuildId;

		// Token: 0x040005C6 RID: 1478
		public List<BuildRegionParams> BuildRegions;

		// Token: 0x040005C7 RID: 1479
		public Dictionary<string, string> CustomTags;
	}
}
