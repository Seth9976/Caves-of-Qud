using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class EntityProfileBody : PlayFabBaseModel
	{
		// Token: 0x04000300 RID: 768
		public string AvatarUrl;

		// Token: 0x04000301 RID: 769
		public DateTime Created;

		// Token: 0x04000302 RID: 770
		public string DisplayName;

		// Token: 0x04000303 RID: 771
		public EntityKey Entity;

		// Token: 0x04000304 RID: 772
		public string EntityChain;

		// Token: 0x04000305 RID: 773
		public List<string> ExperimentVariants;

		// Token: 0x04000306 RID: 774
		public Dictionary<string, EntityProfileFileMetadata> Files;

		// Token: 0x04000307 RID: 775
		public string Language;

		// Token: 0x04000308 RID: 776
		public string LeaderboardMetadata;

		// Token: 0x04000309 RID: 777
		public EntityLineage Lineage;

		// Token: 0x0400030A RID: 778
		public Dictionary<string, EntityDataObject> Objects;

		// Token: 0x0400030B RID: 779
		public List<EntityPermissionStatement> Permissions;

		// Token: 0x0400030C RID: 780
		public Dictionary<string, EntityStatisticValue> Statistics;

		// Token: 0x0400030D RID: 781
		public int VersionNumber;
	}
}
