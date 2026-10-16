using System;
using PlayFab.SharedModels;

namespace PlayFab.AuthenticationModels
{
	// Token: 0x0200033A RID: 826
	[Serializable]
	public class EntityLineage : PlayFabBaseModel
	{
		// Token: 0x04001223 RID: 4643
		public string CharacterId;

		// Token: 0x04001224 RID: 4644
		public string GroupId;

		// Token: 0x04001225 RID: 4645
		public string MasterPlayerAccountId;

		// Token: 0x04001226 RID: 4646
		public string NamespaceId;

		// Token: 0x04001227 RID: 4647
		public string TitleId;

		// Token: 0x04001228 RID: 4648
		public string TitlePlayerAccountId;
	}
}
