using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000254 RID: 596
	[Serializable]
	public class GrantCharacterToUserRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CA5 RID: 3237
		public string CatalogVersion;

		// Token: 0x04000CA6 RID: 3238
		public string CharacterName;

		// Token: 0x04000CA7 RID: 3239
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000CA8 RID: 3240
		public string ItemId;
	}
}
