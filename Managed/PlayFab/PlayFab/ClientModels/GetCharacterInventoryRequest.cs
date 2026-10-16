using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001FB RID: 507
	[Serializable]
	public class GetCharacterInventoryRequest : PlayFabRequestCommon
	{
		// Token: 0x04000BDB RID: 3035
		public string CatalogVersion;

		// Token: 0x04000BDC RID: 3036
		public string CharacterId;

		// Token: 0x04000BDD RID: 3037
		public Dictionary<string, string> CustomTags;
	}
}
