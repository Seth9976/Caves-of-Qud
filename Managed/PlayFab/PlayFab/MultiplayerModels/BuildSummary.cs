using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class BuildSummary : PlayFabBaseModel
	{
		// Token: 0x04000395 RID: 917
		public string BuildId;

		// Token: 0x04000396 RID: 918
		public string BuildName;

		// Token: 0x04000397 RID: 919
		public DateTime? CreationTime;

		// Token: 0x04000398 RID: 920
		public Dictionary<string, string> Metadata;

		// Token: 0x04000399 RID: 921
		public List<BuildRegion> RegionConfigurations;
	}
}
