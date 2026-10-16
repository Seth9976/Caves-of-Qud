using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	public class EntityStatisticValue : PlayFabBaseModel
	{
		// Token: 0x04000315 RID: 789
		public Dictionary<string, EntityStatisticChildValue> ChildStatistics;

		// Token: 0x04000316 RID: 790
		public string Metadata;

		// Token: 0x04000317 RID: 791
		public string Name;

		// Token: 0x04000318 RID: 792
		public int? Value;

		// Token: 0x04000319 RID: 793
		public int Version;
	}
}
