using System;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public class EntityLineage : PlayFabBaseModel
	{
		// Token: 0x040002F4 RID: 756
		public string CharacterId;

		// Token: 0x040002F5 RID: 757
		public string GroupId;

		// Token: 0x040002F6 RID: 758
		public string MasterPlayerAccountId;

		// Token: 0x040002F7 RID: 759
		public string NamespaceId;

		// Token: 0x040002F8 RID: 760
		public string TitleId;

		// Token: 0x040002F9 RID: 761
		public string TitlePlayerAccountId;
	}
}
